using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public abstract class ModelViewModelBase : ViewModelBase
    {
        public const string ID_PARAMETER = nameof(ID_PARAMETER);

        public ModelViewModelBase() { }

        public ModelViewModelBase(string path, bool isNew) : base(path, isNew) { }
    }

    public abstract class ModelViewModelBase<TModel, TEditViewModel> : ModelViewModelBase, IQueryAttributable
        where TModel : class, IIdentifiable, new()
        where TEditViewModel : IViewModel
    {
        private readonly AsyncRelayCommand _editCommand;
        protected TModel _value = new();

        public ModelViewModelBase()
        {
            _editCommand = new(EditAsync);
        }

        public ModelViewModelBase(string path, bool isNew) : base(path, isNew)
        {
            _editCommand = new(EditAsync);
        }

        protected abstract IService<TModel> Service { get; }

        public AsyncRelayCommand EditCommand => _editCommand;

        private async Task EditAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [EditViewModelBase.ID_PARAMETER] = _value.Id,
                [EditViewModelBase.IS_NEW_PARAMETER] = false
            };
            AddEditRouteParameters(parameters);
            await GoToAsync<TEditViewModel>(parameters);
        }

        protected virtual void AddEditRouteParameters(Dictionary<string, object> parameters) { }

        public override async Task ApplyQueryAttributesAsync(IDictionary<string, object> query)
        {
            if (query.TryGetValue(ID_PARAMETER, out object? value) && value is int id)
            {
                await SetValueAsync(id);
            }
            await base.ApplyQueryAttributesAsync(query);
        }

        private async Task SetValueAsync(int id)
        {
            (bool suceeded, TModel value) = await Service.TryGetAsync(id);
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