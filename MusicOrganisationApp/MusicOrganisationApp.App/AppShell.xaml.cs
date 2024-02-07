using MusicOrganisationApp.Lib.ViewModels;

namespace MusicOrganisationApp.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            StartupViewModel viewModel = (StartupViewModel)BindingContext;
            await viewModel.Init();
        }
    }
}