using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Viewmodels;
using MusicOrganisation.Lib.ViewModels.EditViewModels;

namespace MusicOrganisation.Lib.ViewModels.ModelViewModels
{
    public abstract class ModelViewModelBase<T> : ViewModelBase, IQueryAttributable where T : class, ITable, new()
    {
        public const string ID_PARAMETER = nameof(ID_PARAMETER);

        private readonly string _editViewModelRoute;

        protected T _value;

        private readonly AsyncRelayCommand _editCommand;

        public ModelViewModelBase(string editViewModelRoute)
        {
            _editViewModelRoute = editViewModelRoute;
            _value = new();
            _editCommand = new(EditAsync);
        }

        public AsyncRelayCommand EditCommand => _editCommand;

        private async Task EditAsync()
        {
            if (_value is not null)
            {
                Dictionary<string, object> parameters = new()
                {
                    [EditViewModelBase<T>.ID_PARAMETER] = _value.Id,
                    [EditViewModelBase<T>.IS_NEW_PARAMETER] = false,
                };
                await GoToAsync(parameters, _editViewModelRoute);
            }
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(ID_PARAMETER, out object? value) && value is int id)
            {
                await SetValue(id);
            }
        }

        private async Task SetValue(int id)
        {
            T value = await _service.GetAsync<T>(id);
            _value = value;
            SetDisplayValues();
        }

        protected abstract void SetDisplayValues();
    }
}