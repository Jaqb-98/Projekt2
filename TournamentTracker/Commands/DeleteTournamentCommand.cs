using Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TournamentTracker.ViewModels;
using TournamentTracker.Views;

namespace TournamentTracker.Commands
{
    public class DeleteTournamentCommand : ICommand
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

            var vm = parameter as SelectTournamentViewModel;

            return (vm.SelectedTournament != null);
           
        }

        public void Execute(object parameter)
        {
            var vm = parameter as SelectTournamentViewModel;

            using (var context = new DataContext())
            {
                var tournamentToDelete = context.Tournaments.Where(t => t.Id == vm.SelectedTournament.Id).FirstOrDefault();
                try
                {
                    context.Tournaments.Remove(tournamentToDelete);
                    context.SaveChanges();
                    MessageBox.Show($"{tournamentToDelete.TournamentName} has been deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {

                    MessageBox.Show($"Error","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
