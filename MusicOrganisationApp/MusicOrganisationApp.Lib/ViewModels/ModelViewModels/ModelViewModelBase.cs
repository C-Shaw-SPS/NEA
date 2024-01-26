using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public abstract class ModelViewModelBase : ViewModelBase
    {
        public const string ID_PARAMETER = nameof(ID_PARAMETER);
    }

    public abstract class ModelViewModelBase<T> : ModelViewModelBase, IQueryAttributable where T : class, IIdentifiable, new()
    {
        private readonly string _editPath;
        private readonly AsyncRelayCommand _editCommand;
        private T _value;

        public ModelViewModelBase(string editPath)
        {
            _editCommand = new(EditAsync);
            _editPath = editPath;
            _value = new();
        }

        protected abstract IService<T> Service { get; } 

        public AsyncRelayCommand EditCommand => _editCommand;

        private async Task EditAsync()
        {
            throw new NotImplementedException();
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(ID_PARAMETER, out object? value) && value is int id)
            {
                await SetValueAsync(id);
            }
        }

        private async Task SetValueAsync(int id)
        {
            (bool suceeded, T value) = await Service.TryGetAsync(id);
            if (suceeded)
            {
                _value = value;
                SetDisplayValues();
            }
            else
            {
                await GoBackAsync();
            }
        }

        protected abstract void SetDisplayValues();
    }
}