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
    public class AddTeamCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            var team = parameter as string;
            if (team is null)
                return false;
            if (string.IsNullOrWhiteSpace(team))
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            var teamName = parameter as string;
            var team = new Team() { Name = teamName };

            using (var context = new DataContext())
            {


                try
                {
                    context.Teams.Add(team);
                    context.SaveChanges();
                    MessageBox.Show($"{team.Name} has been added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"There is already team named {team.Name}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }
    }
}
