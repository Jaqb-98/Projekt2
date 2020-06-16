using Caliburn.Micro;
using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TournamentTracker.Commands;

namespace TournamentTracker.ViewModels
{
    public class NewTeamViewModel : Screen, IPageViewModel
    {
        private string _teamName;

        public string TeamName
        {
            get { return _teamName; }
            set
            {
                _teamName = value;
                NotifyOfPropertyChange(() => TeamName);
            }
        }

        public string VMName { get { return string.Format("NewTeam"); } }

        public ICommand AddNewTeam { get; private set; }

        public NewTeamViewModel()
        {
            AddNewTeam = new AddTeamCommand();
        }


        public void BackToNewTournament()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItem(new NewTournamentViewModel());
        }





    }
}
