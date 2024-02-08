using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public abstract partial class CollectionViewModelBase<TModel, TModelViewModel, TEditViewModel> : ViewModelBase
        where TModel : class, IIdentifiable, new()
        where TModelViewModel : IViewModel
        where TEditViewModel : IViewModel
    {
        private readonly AsyncRelayCommand _selectCommand;
        private readonly AsyncRelayCommand _addNewCommand;

        [ObservableProperty]
        private ObservableCollection<TModel> _collection = [];

        [ObservableProperty]
        private TModel? _selectedItem;

        public CollectionViewModelBase()
        {
            _selectCommand = new(SelectAsync);
            _addNewCommand = new(AddNewAsync);
        }

        protected abstract IService<TModel> Service { get; }

        public AsyncRelayCommand SelectCommand => _selectCommand;

        public AsyncRelayCommand AddNewCommand => _addNewCommand;

        public virtual async Task RefreshAsync()
        {
            IEnumerable<TModel> items = await Service.GetAllAsync();
            ResetCollection(Collection, items);
        }

        private async Task SelectAsync()
        {
            if (SelectedItem is not null)
            {
                Dictionary<string, object> paramters = new()
                {
                    [ModelViewModelBase.ID_PARAMETER] = SelectedItem.Id
                };
                await GoToAsync<TModelViewModel>(paramters);
            }
        }

        private async Task AddNewAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [EditViewModelBase.IS_NEW_PARAMETER] = true
            };
            AddAddNewParameters(parameters);
            await GoToAsync<TEditViewModel>(parameters);
        }

        protected virtual void AddAddNewParameters(Dictionary<string, object> parameters) { }
    }
}