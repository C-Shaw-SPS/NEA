using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public abstract class EditViewModelBase : ViewModelBase
    {
        public const string ID_PARAMETER = nameof(ID_PARAMETER);
        public const string IS_NEW_PARAMETER = nameof(IS_NEW_PARAMETER);
    }

    public abstract partial class EditViewModelBase<T> : EditViewModelBase, IQueryAttributable where T : class, IIdentifiable, new()
    {
        private readonly string _editPageTitle;
        private readonly string _newPageTitle;

        private int _id;
        private bool _isNew;
        private T? _value;

        private readonly AsyncRelayCommand _trySaveCommand;
        private readonly AsyncRelayCommand _deleteCommand;

        [ObservableProperty]
        private string _pageTitle;

        [ObservableProperty]
        private bool _canDelete;

        public EditViewModelBase(string editPageTitle, string newPageTitle)
        {
            _editPageTitle = editPageTitle;
            _newPageTitle = newPageTitle;

            _isNew = false;
            _value = new();

            _pageTitle = _editPageTitle;
            _canDelete = true;

            _trySaveCommand = new(TrySaveAsync);
            _deleteCommand = new(DeleteAsync);
        }

        private async Task TrySaveAsync()
        {
            throw new NotImplementedException();
        }

        private async Task DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
    }
}