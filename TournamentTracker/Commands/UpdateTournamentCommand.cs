using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TournamentTracker.ViewModels;

namespace TournamentTracker.Commands
{
    public class UpdateTournamentCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object parameter)
        {
            if (parameter == null)
                return false;

            var vm = parameter as TournamentViewModel;

            if (vm.SelectedMatchup == null)
                return false;


            var matchup = vm.SelectedMatchup;


            var entry1Score = matchup.Entries.FirstOrDefault().Score;
            var entry2Score = matchup.Entries.Skip(1).FirstOrDefault().Score;
            if (entry1Score == entry2Score)
                return false;

            
            return true;


        }

        public void Execute(object parameter)
        {
            var vm = parameter as TournamentViewModel;

            var tournament = vm.Tournament;
            var matchup = vm.SelectedMatchup;

            UpdateMatchup(matchup);
        }

        private void UpdateMatchup(Matchup matchup)
        {
            using (var context = new DataContext())
            {
               
                var identry1 = matchup.Entries.FirstOrDefault().Id;
                var identry2 = matchup.Entries.Skip(1).FirstOrDefault().Id;
                var entry1 = context.MatchupEntries.Where(e => e.Id == identry1).FirstOrDefault();
                var entry2 = context.MatchupEntries.Where(e => e.Id == identry2).FirstOrDefault();

                var entry1Score = matchup.Entries.FirstOrDefault().Score;
                var entry2Score = matchup.Entries.Skip(1).FirstOrDefault().Score;
                entry1.Score = entry1Score;
                entry2.Score = entry2Score;

                Team team;
                if (entry1Score > entry2Score)
                    team = entry1.TeamCompeting;
                else
                    team = entry2.TeamCompeting;

                var matchupFromDB = context.Matchups.Where(m => m.Id == matchup.Id).FirstOrDefault();
                matchupFromDB.Winner = team;

                context.SaveChanges();

            }
        }


    }
}
