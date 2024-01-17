using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Services;

namespace MusicOrganisation.Lib.Viewmodels
{
    public abstract class ViewModelBase : ObservableObject
    {
        protected const string _RETURN = "..";

        protected readonly string _databasePath;

        protected readonly Service _service;

        public ViewModelBase()
        {
            _databasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseProperties.NAME);
            _service = new(_databasePath);
        }

        protected static async Task GoToAsync(Dictionary<string, object> parameters, params string[] routes)
        {
            string route = GetRoute(routes);
            await Shell.Current.GoToAsync(route, parameters);
        }

        protected static async Task GoToAsync(params string[] routes)
        {
            string route = GetRoute(routes);
            await Shell.Current.GoToAsync(route);
        }

        private static string GetRoute(IEnumerable<string> routes)
        {
            string route = string.Join('/', routes);
            return route;
        }
    }
}