using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;

namespace MusicOrganisationApp.App.Views.CollectionViews
{
    public partial class AllRepertoirePage : ContentPage
    {
        public AllRepertoirePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            AllRepertoireViewModel viewModel = (AllRepertoireViewModel)BindingContext;
            await viewModel.RefreshAsync();
        }
    }
}