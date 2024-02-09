using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels
{
    public interface IViewModel
    {
        public abstract static string Route { get; }

        public static void ResetCollection<T>(ObservableCollection<T> displayedCollection, IEnumerable<T> newCollection)
        {
            displayedCollection.Clear();
            foreach (T item in newCollection)
            {
                displayedCollection.Add(item);
            }
        }
    }
}