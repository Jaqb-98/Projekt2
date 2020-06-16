using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TournamentTracker.ViewModels;

namespace TournamentTracker.Commands
{
    public class CreateNewTournamentCommand : ICommand
    {


        private Tournament _tournament;
        private List<Team> _teams = new List<Team>();
        private int _byes;
        private int _numberOfTeams;
        private int _numberOfMatchups;
        private int _numberOfRounds;
        private List<int> _teamIDs = new List<int>();

        public CreateNewTournamentCommand()
        {

        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }




        public bool CanExecute(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            var vm = parameter as NewTournamentViewModel;
            int teams = vm.SelectedTeams.Count;

            if (string.IsNullOrEmpty(vm.TournamentName))
                return false;

            return ((teams != 0) && ((teams & (teams - 1)) == 0)) && teams > 1;

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

        private int CalculateNumberOfByes(int rounds, int teamsCount)
        {
            int byes = 0;
            int teams = 1;

            for (int i = 1; i <= rounds; i++)
            {
                teams *= 2;
            }

            byes = teams - teamsCount;

            return byes;
        }

        public void Execute(object parameter)
        {
            NewTournamentViewModel vm = parameter as NewTournamentViewModel;

            ICollection<Team> selectedTeams = vm.SelectedTeams;

            string tournamentName = vm.TournamentName;

            foreach (var team in selectedTeams)
            {
                _teamIDs.Add(team.Id);
            }


            _numberOfTeams = selectedTeams.Count;
            _numberOfMatchups = _numberOfTeams - 1;
            _numberOfRounds = CalculateNumberOfRounds(teamCount: selectedTeams.Count);
            _byes = CalculateNumberOfByes(_numberOfRounds, _numberOfTeams);

            _tournament = new Tournament()
            {
                TournamentName = tournamentName,
            };

            AddTournament();


        }

        private void AddTournament()
        {
            using (var context = new DataContext())
            {
                var teams = context.Teams.Where(t => _teamIDs.Contains(t.Id)).ToList();
                _tournament.EnteredTeams = teams;
                foreach (var matchup in CreateFirstRoundMatchups(_byes, teams))
                {
                    _tournament.Matchups.Add(matchup);
                }

                CreateRestOfTheMatchups();

                try
                {
                    context.Tournaments.Add(_tournament);
                    context.SaveChanges();
                    MessageBox.Show($"{_tournament.TournamentName} has been added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"There is already tournament named {_tournament.TournamentName}","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
        }

    

        private List<Matchup> CreateFirstRoundMatchups(int byes, List<Team> teams)
        {

            List<Matchup> matchups = new List<Matchup>();
            Matchup currentMatchup = new Matchup();

            foreach (var team in teams)
            {
                currentMatchup.Entries.Add(new MatchupEntry() { TeamCompeting = team });

                if (byes > 0 || currentMatchup.Entries.Count > 1)
                {
                    currentMatchup.MatchupRound = 1;
                    matchups.Add(currentMatchup);
                    currentMatchup = new Matchup();

                    if (byes > 0)
                        byes -= 1;
                }
            }
            return matchups;

        }

        private void CreateRestOfTheMatchups()
        {

            using (var context = new DataContext())
            {

                int round = 2;
                List<Matchup> previousRound = _tournament.Matchups.Where(m => m.MatchupRound == 1).ToList();
                List<Matchup> currentRound = new List<Matchup>();
                Matchup currentMatchup = new Matchup();

                while (round <= _numberOfRounds)
                {
                    foreach (var matchup in previousRound)
                    {
                        currentMatchup.Entries.Add(new MatchupEntry() { });

                        if (currentMatchup.Entries.Count > 1)
                        {
                            currentMatchup.MatchupRound = round;
                            currentRound.Add(currentMatchup);
                            currentMatchup = new Matchup();
                        }
                    }

                    foreach (var matchup in currentRound)
                    {
                        _tournament.Matchups.Add(matchup);
                    }

                    previousRound = currentRound;

                    currentRound = new List<Matchup>();

                    round++;
                }
            }
        }

   

    }
}
