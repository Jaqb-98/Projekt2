using Caliburn.Micro;
using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TournamentTracker.Commands;
using System.Threading;

namespace TournamentTracker.ViewModels
{
    public class SelectTournamentViewModel: Screen, IPageViewModel
    {
        public string VMName => throw new NotImplementedException();

        public ICommand DeleteTournamentCommand { get; set; }

        private Tournament _selectedTournament;
        private BindableCollection<Tournament> _allTournaments;

        public void StartTournament()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItem(new TournamentViewModel(SelectedTournament.Id));

        }

        public BindableCollection<Tournament> AllTournaments
        {
            get { return _allTournaments; }
            set
            {
                _allTournaments = value;
                NotifyOfPropertyChange(() => AllTournaments);
            }
        }

        public void RefreshTournaments()
        {
            

        }
        public Tournament SelectedTournament
        {
            get { return _selectedTournament; }
            set
            {
                _selectedTournament = value;
                NotifyOfPropertyChange(() => SelectedTournament);
            }
        }

        public SelectTournamentViewModel()
        {
            AllTournaments = GetTournaments();
            DeleteTournamentCommand = new DeleteTournamentCommand();
        
        }

        private BindableCollection<Tournament> GetTournaments()
        {
            using (var context = new DataContext())
            {
                var tournaments = context.Tournaments
                   .Include(t => t.EnteredTeams)
                   .Include(t => t.EnteredTeams.Select(x=>x.Entries))
                   .Include(t=>t.EnteredTeams.Select(x=>x.Tournaments))
                   .Include(t=>t.Matchups)
                   .Include(t=>t.Matchups.Select(x=>x.Entries))
                   .ToList();

               
                return new BindableCollection<Tournament>(tournaments);          
            }
        }

        public void BackToMenu()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItem(new MenuViewModel());
        }
    }
}
