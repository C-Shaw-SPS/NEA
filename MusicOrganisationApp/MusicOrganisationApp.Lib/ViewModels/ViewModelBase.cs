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
        private readonly string _path;
        protected readonly DatabaseConnection _database;

        public ViewModelBase()
        {
            _path = Path.Combine(FileSystem.AppDataDirectory, DatabaseProperties.NAME);
            _database = new(_path);
        }
    }
}