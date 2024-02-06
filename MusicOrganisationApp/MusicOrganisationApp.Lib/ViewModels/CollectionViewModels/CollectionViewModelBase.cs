using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public abstract partial class CollectionViewModelBase<T> : ViewModelBase where T : class, IIdentifiable, new()
    {
        private readonly string _modelRoute;
        private readonly string _editRoute;
        private readonly AsyncRelayCommand _selectCommand;
        private readonly AsyncRelayCommand _addNewCommand;

        [ObservableProperty]
        private ObservableCollection<T> _collection = [];

        [ObservableProperty]
        private T? _selectedItem;

        public CollectionViewModelBase(string modelRoute, string editRoute)
        {
            _modelRoute = modelRoute;
            _editRoute = editRoute;

            _selectCommand = new(SelectAsync);
            _addNewCommand = new(AddNewAsync);
        }

        protected abstract IService<T> Service { get; }



        public AsyncRelayCommand SelectCommand => _selectCommand;

        public AsyncRelayCommand AddNewCommand => _addNewCommand;

        public virtual async Task RefreshAsync()
        {
            IEnumerable<T> items = await Service.GetAllAsync();
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
                await GoToAsync(paramters, _modelRoute);
            }
        }

        private async Task AddNewAsync()
        {
            Dictionary<string, object> paramters = new()
            {
                [EditViewModelBase.IS_NEW_PARAMETER] = true
            };
            AddAddNewParameters(paramters);
            await GoToAsync(paramters, _editRoute);
        }

        protected virtual void AddAddNewParameters(Dictionary<string, object> parameters) { }
    }
}