using Caliburn.Micro;
using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using TournamentTracker.Commands;

namespace TournamentTracker.ViewModels
{
    public class NewTournamentViewModel : Screen, IPageViewModel
    {


        public string VMName => $"NewTournamentVM";

        public ICommand CreateNewTournamentCommand { get; set; }

        private string _filterText;

        public string FilterText
        {
            get { return _filterText; }
            set {
                _filterText = value;
                collection.View.Refresh();
                NotifyOfPropertyChange(() => FilterText);
            }
        }

        public ICollectionView SourceCollection { get { return collection.View; } }

        private CollectionViewSource collection;

        public void collection_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            Team team = e.Item as Team;
            if (team.Name.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }


        private BindableCollection<Team> _allTeams;
        private ObservableCollection<Team> _selectedTeams;
        private string _tournamentName;

        public string TournamentName
        {
            get { return _tournamentName; }
            set {
                _tournamentName = value;
                NotifyOfPropertyChange(() => TournamentName);
            }
        }

    



        public BindableCollection<Team> AllTeams
        {
            get { return _allTeams; }
            set
            {
                _allTeams = value;
                NotifyOfPropertyChange(() => AllTeams);
            }
        }


        public ObservableCollection<Team> SelectedTeams
        {
            get { return _selectedTeams; }
            set
            {
                _selectedTeams = value;
                NotifyOfPropertyChange(() => SelectedTeams);
            }
        }



        public NewTournamentViewModel()
        {
            AllTeams = GetAllTeams();
            SelectedTeams = new ObservableCollection<Team>();
            CreateNewTournamentCommand = new CreateNewTournamentCommand();
            collection = new CollectionViewSource();
            collection.Source = AllTeams;
            collection.Filter += collection_Filter;
        }

        private BindableCollection<Team> GetAllTeams()
        {
            using (var context = new DataContext())
            {
                var teams = context.Teams
                    .Include(t=>t.Tournaments)
                    .ToList();
                return new BindableCollection<Team>(teams);
            }
        }



        public void LoadNewTeamPage()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItem(new NewTeamViewModel());
        }

        public void BackToMenu()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItem(new MenuViewModel());
        }
    }
}
