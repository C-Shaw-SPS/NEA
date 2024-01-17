using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.ViewModels.EditViewModels;
using MusicOrganisation.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisation.Lib.ViewModels.CollectionViewModels
{
    internal class AllWorksViewModel : CollectionViewModelBase<WorkData>
    {
        private static readonly Dictionary<string, string> _orderings = new()
        {
            { "Title", nameof(WorkData.Title) },
            { "Composer", nameof(WorkData.Genre) }
        };

        public AllWorksViewModel() : base(_orderings, WorkViewModel.ROUTE, EditWorkViewModel.ROUTE, nameof(WorkData.Title))
        {

        }
    }
}