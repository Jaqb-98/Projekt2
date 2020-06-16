using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TournamentTracker.ViewModels;

namespace TournamentTracker.Commands
{
    public class AdvanceWinnersCommand : ICommand
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

            var winners = vm.WinnersCount;

            if (vm.Round  < vm.Rounds.Count)
                return winners == vm.Rounds[vm.Round - 1].Count;

            return false;


        }

        public void Execute(object parameter)
        {
            using (var context = new DataContext())
            {

                var vm = parameter as TournamentViewModel;
                var tournament = vm.Tournament;
                var winners = new List<Team>();
                var currentRound = vm.Rounds[vm.Round - 1];
                var nextRound = vm.Rounds[vm.Round];

                foreach (var matchup in vm.Rounds[vm.Round - 1])
                {
                    winners.Add(context.Teams.Where(t => t.Id == matchup.Winner.Id).FirstOrDefault());
                }

                int index = 0;

                foreach (var matchup in nextRound)
                {
                    foreach (var entry in matchup.Entries)
                    {
                        var a = context.MatchupEntries.Where(e => e.Id == entry.Id).FirstOrDefault();
                        a.TeamCompeting = winners[index];
                        //entry.TeamCompeting = winners[index];
                        index++;
                    }
                }

                context.SaveChanges();
            }
        }

    }
}
