using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Databases;

namespace MusicOrganisationApp.Lib.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        protected const string _GO_BACK = "..";

        private readonly string _path;
        private readonly bool _isTesting;
        protected readonly DatabaseConnection _database;

        public ViewModelBase()
        {
            _path = Path.Combine(FileSystem.AppDataDirectory, DatabaseProperties.NAME);
            _isTesting = false;
            _database = new(_path);
        }

        public ViewModelBase(string path, bool isTesting)
        {
            _path = SqlFormatting.FormatAsDatabasePath(path);
            _isTesting = isTesting;
            _database = new(path);
        }

        protected async Task GoToAsync(Dictionary<string, object> parameters, params string[] routes)
        {
            if (!_isTesting)
            {
                string route = GetRoute(routes);
                await Shell.Current.GoToAsync(route, parameters);
            }
        }

        protected  async Task GoToAsync(params string[] routes)
        {
            if (!_isTesting)
            {
                string route = GetRoute(routes);
                await Shell.Current.GoToAsync(route);
            }
        }

        protected async Task GoBackAsync()
        {
            await GoToAsync(_GO_BACK);
        }

        private static string GetRoute(IEnumerable<string> routes)
        {
            return string.Join('/', routes);
        }
    }
}