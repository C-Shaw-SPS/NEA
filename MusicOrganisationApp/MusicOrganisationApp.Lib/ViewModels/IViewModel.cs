using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels
{
    public interface IViewModel
    {
        public abstract static string Route { get; }
    }
}