using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TournamentTracker.ViewModels;

namespace TournamentTracker.Views
{
    /// <summary>
    /// Interaction logic for NewTournamentView.xaml
    /// </summary>
    public partial class NewTournamentView : UserControl
    {
        //NewTournamentViewModel vm = new NewTournamentViewModel();
        public NewTournamentView()
        {
            InitializeComponent();
            teamList.SelectionChanged += TeamListSelectionChanged;
        }

        private static void TeamListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox == null) return;

            var viewModel = listbox.DataContext as NewTournamentViewModel;
            if (viewModel == null) return;

            viewModel.SelectedTeams.Clear();

            foreach (Team item in listbox.SelectedItems)
            {
                viewModel.SelectedTeams.Add(item);
            }
        }



    }
}
