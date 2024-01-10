using MusicOrganisation.App.Views;
using MusicOrganisation.Lib.ViewModels;

namespace MusicOrganisation.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(AllComposersViewModel.ROUTE, typeof(AllComposersPage));
        }
    }
}