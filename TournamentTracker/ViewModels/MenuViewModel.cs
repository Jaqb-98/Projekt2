using Caliburn.Micro;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace TournamentTracker.ViewModels
{
    public class MenuViewModel : Screen, IPageViewModel
    {

        public MenuViewModel()
        {
            
        }

        public string VMName { get { return this.GetType().Name; } }

        public void LoadNewTournamentPage()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItem(new NewTournamentViewModel());
        }

        public void LoadTournamentPage()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItem(new SelectTournamentViewModel());
        }
    }
}
