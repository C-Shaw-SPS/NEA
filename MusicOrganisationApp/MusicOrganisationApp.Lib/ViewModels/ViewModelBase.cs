﻿using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Databases;
using System.Collections.ObjectModel;

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

        protected async Task GoToAsync<TViewModel>(Dictionary<string, object> parameters) where TViewModel : IViewModel
        {
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

        private async Task GoToAsync(string route)
        {
            if (_isTesting)
            {
                await Shell.Current.GoToAsync(route);
            }
        }

        private async Task GoToAsync(string route, Dictionary<string, object> parameters)
        {
            if (!_isTesting)
            {
                await Shell.Current.GoToAsync(route, parameters);
            }
        }

        protected static void ResetCollection<T>(ObservableCollection<T> displayedCollection, IEnumerable<T> newCollection)
        {
            displayedCollection.Clear();
            foreach (T item in newCollection)
            {
                displayedCollection.Add(item);
            }
        }
    }
}