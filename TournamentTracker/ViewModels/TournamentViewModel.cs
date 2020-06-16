using Caliburn.Micro;
using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TournamentTracker.Commands;

namespace TournamentTracker.ViewModels
{
    public class TournamentViewModel : Screen, IPageViewModel
    {
        public string VMName => throw new NotImplementedException();

        #region PrivateVariables
        private int _rounds;
        private int _round = 1;
        private List<Matchup> _allMatchups;
        private Matchup _selectedMatchup;
        private List<Matchup> _matchups;
        private Tournament _tournament;
        #endregion

        #region props

        public List<Matchup> AllMatchups
        {
            get { return _allMatchups; }
            set
            {
                _allMatchups = value;
                NotifyOfPropertyChange(() => AllMatchups);
            }
        }
        public int Round
        {
            get { return _round; }
            set
            {
                _round = value;
                NotifyOfPropertyChange(() => Round);
            }
        }

        public Matchup SelectedMatchup
        {
            get { return _selectedMatchup; }
            set
            {
                _selectedMatchup = value;
                NotifyOfPropertyChange(() => SelectedMatchup);
            }
        }
        public List<List<Matchup>> Rounds { get; set; }


        public List<Matchup> Matchups
        {
            get { return _matchups; }
            set
            {
                _matchups = value;
                NotifyOfPropertyChange(() => Matchups);
            }
        }

        public Tournament Tournament
        {
            get { return _tournament; }
            set
            {
                _tournament = value;
                NotifyOfPropertyChange(() => Tournament);
            }
        }
        #endregion

        #region Commands
        public ICommand UpdateTournamentCommand { get; set; }
        public ICommand AdvanceWinnersCommand { get; set; }
        #endregion


        #region ctor

        public TournamentViewModel(int tournamentID)
        {
            Tournament = GetTournament(tournamentID);
            _rounds = CalculateNumberOfRounds(Tournament.EnteredTeams.Count);
            AllMatchups = Tournament.Matchups.ToList();
            UpdateTournamentCommand = new UpdateTournamentCommand();
            AdvanceWinnersCommand = new AdvanceWinnersCommand();
            Rounds = GetRounds(AllMatchups);
            Matchups = Rounds[0];
        }
        #endregion

        private int _winnersCount;

        public int WinnersCount
        {
            get 
            {
                int count = 0;
                foreach (var matchup in Rounds[Round-1])
                {
                    if (matchup.Winner != null)
                        count++;
                }
                return count;
            }
            set { 
                _winnersCount = value;
                NotifyOfPropertyChange(() => WinnersCount);
            }
        }



        #region Methods

        public void AdvanceWinners()
        {
            var winners = new List<Team>();
            foreach (var matchup in Rounds[Round - 1])
            {
                winners.Add(matchup.Winner);
            }


            int index = 0;
            foreach (var matchup in Rounds[Round])
            {
                foreach (var entry in matchup.Entries)
                {
                    entry.TeamCompeting = winners[index];
                    index++;
                }
            }
        }

        public void UpdateTournamnet()
        {
            var matchupID = SelectedMatchup.Id;
            foreach (var matchup in Rounds[Round - 1])
            {
                if (matchup.Id == matchupID)
                {

                    if (matchup.Entries.FirstOrDefault().Score > matchup.Entries.Skip(1).FirstOrDefault().Score)
                        matchup.Winner = matchup.Entries.FirstOrDefault().TeamCompeting;
                    else
                        matchup.Winner = matchup.Entries.Skip(1).FirstOrDefault().TeamCompeting;
                }

            }

        }
        private Tournament GetTournament(int tournamentID)
        {
            using (var context = new DataContext())
            {
                return context.Tournaments
                   .Include(t => t.EnteredTeams)
                   .Include(t => t.EnteredTeams.Select(x => x.Entries))
                   .Include(t => t.EnteredTeams.Select(x => x.Tournaments))
                   .Include(t => t.Matchups)
                   .Include(t => t.Matchups.Select(x => x.Entries))
                   .Where(t => t.Id == tournamentID)
                   .FirstOrDefault();
            }
        }


        private int CalculateNumberOfRounds(int teamCount)
        {
            int rounds = 1;
            int value = 2;

            while (value < teamCount)
            {
                rounds += 1;
                value *= 2;
            }
            return rounds;
        }

        public void NextRound()
        {
            if (Round < _rounds)
                Round++;

            Matchups = Rounds[Round - 1];
        }

        public void PreviousRound()
        {
            if (Round > 1)
                Round--;

            Matchups = Rounds[Round - 1];
        }




        private List<List<Matchup>> GetRounds(List<Matchup> allMatchups)
        {
            List<List<Matchup>> rounds = new List<List<Matchup>>();

            List<Matchup> round = new List<Matchup>();
            int roundCount = 1;

            for (int i = 0; i < _rounds; i++)
            {

                foreach (var matchup in allMatchups)
                {
                    if (matchup.MatchupRound == roundCount)
                    {
                        round.Add(matchup);
                    }
                }
                roundCount++;
                rounds.Add(round);
                round = new List<Matchup>();
            }

            return rounds;


        }


        public void Back()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItem(new SelectTournamentViewModel());
        }
        #endregion

    }
}
