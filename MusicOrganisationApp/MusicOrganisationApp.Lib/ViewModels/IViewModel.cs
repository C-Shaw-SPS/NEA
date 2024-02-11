using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels
{
    public interface IViewModel
    {
        public abstract static string Route { get; }

        public static void ResetCollection<T>(ObservableCollection<T> observableCollection, IEnumerable<T> values)
        {
            observableCollection.Clear();
            foreach (T item in values)
            {
                observableCollection.Add(item);
            }
        }
    }
}