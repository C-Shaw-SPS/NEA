using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        protected const string _RETURN = "..";

        private readonly string _path;
        protected readonly DatabaseConnection _database;

        public ViewModelBase()
        {
            _path = Path.Combine(FileSystem.AppDataDirectory, DatabaseProperties.NAME);
            _database = new(_path);
        }

        protected static async Task GoToAsync(Dictionary<string, object?> parameters, params string[] routes)
        {
            string route = GetRoute(routes);
            await Shell.Current.GoToAsync(route, parameters);
        }

        protected static async Task ReturnAsync(Dictionary<string, object?> parameters)
        {
            await GoToAsync(parameters, _RETURN);
        }

        protected static async Task GoToAsync(params string[] routes)
        {
            string route = GetRoute(routes);
            await Shell.Current.GoToAsync(route);
        }

        protected static async Task ReturnAsync()
        {
            await GoToAsync(_RETURN);
        }

        private static string GetRoute(IEnumerable<string> routes)
        {
            return string.Join('/', routes);
        }
    }
}