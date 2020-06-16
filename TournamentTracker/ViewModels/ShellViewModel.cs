using Caliburn.Micro;


namespace TournamentTracker.ViewModels
{
    public class ShellViewModel : Conductor<IPageViewModel>
    {
        public string VMName => $"ShellVM";

        public ShellViewModel()
        {
            ActivateItem(new MenuViewModel());
        }

     
    }
}