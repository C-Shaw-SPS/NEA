using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Databases;

namespace MusicOrganisationApp.Lib.ViewModels
{
    public abstract class ViewModelBase : ObservableObject, IQueryAttributable
    {
        protected const string _GO_BACK = "..";

        private readonly string _path;
        private readonly bool _isTesting;
        private bool _isCurrentViewModel = true;
        protected readonly DatabaseConnection _database;

        public ViewModelBase() : this(GetDefaultPath(), false) { }

        public ViewModelBase(string path, bool isTesting)
        {
            _path = SqlFormatting.FormatAsDatabasePath(path);
            _isTesting = isTesting;
            _database = new(_path);
        }

        public bool IsCurrentViewModel => _isCurrentViewModel;

        private static string GetDefaultPath()
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, DatabaseProperties.NAME);
            return path;
        }

        protected async Task GoToAsync<TViewModel>(Dictionary<string, object> parameters) where TViewModel : IViewModel
        {
            AddParameters(parameters);
            await GoToAsync(TViewModel.Route, parameters);
        }

        protected async Task GoToAsync<TViewModel>() where TViewModel : IViewModel
        {
            await GoToAsync(TViewModel.Route);
        }

        protected async Task GoBackAsync()
        {
            await GoToAsync(_GO_BACK);
        }

        protected async Task GoToPupilDataAsync<TViewModel>(int pupilId) where TViewModel : IPupilDataViewModel
        {
            Dictionary<string, object> parameters = new()
            {
                [IPupilDataViewModel.PUPIL_ID_PARAMETER] = pupilId
            };
            await GoToAsync<TViewModel>(parameters);
        }

        private async Task GoToAsync(string route)
        {
            if (!_isTesting)
            {
                await Shell.Current.GoToAsync(route);
            }
            _isCurrentViewModel = false;
        }

        private async Task GoToAsync(string route, Dictionary<string, object> parameters)
        {
            if (!_isTesting)
            {
                await Shell.Current.GoToAsync(route, parameters);
            }
            _isCurrentViewModel = false;
        }

        public virtual Task ApplyQueryAttributesAsync(IDictionary<string, object> query)
        {
            if (this is IPupilDataViewModel pupilDataViewModel)
            {
                pupilDataViewModel.ApplyPupilAttribute(query);
            }
            return Task.CompletedTask;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            await ApplyQueryAttributesAsync(query);
        }

        private void AddParameters(IDictionary<string, object> parameters)
        {
            if (this is IPupilDataViewModel pupilDataViewModel)
            {
                pupilDataViewModel.AddPupilIdParameter(parameters);
            }
        }
    }
}