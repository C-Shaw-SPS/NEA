using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisation.Lib.Databases;

namespace MusicOrganisation.Lib.Viewmodels
{
    public abstract class ViewModelBase : ObservableObject
    {
        protected string _databasePath;

        public ViewModelBase()
        {
            _databasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseProperties.NAME);
        }
    }
}