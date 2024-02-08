using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public class AllRepertoireViewModel : SearchableCollectionViewModel<Repertoire, RepertoireViewModel, EditRepertoireViewModel>, IQueryAttributable, IPupilDataViewModel
    {
        private const string _ROUTE = nameof(AllRepertoireViewModel);

        private static readonly Dictionary<string, string> _orderings = new()
        {
            ["Date started"] = nameof(Repertoire.DateStarted),
            ["Title"] = nameof(Repertoire.Title),
            ["Not finished"] = nameof(Repertoire.IsFinishedLearning),
            ["Composer"] = nameof(Repertoire.ComposerName)
        };

        private readonly RepertoireService _service;

        public AllRepertoireViewModel() : base(_orderings)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override ISearchService<Repertoire> SearchService => _service;

        public int? PupilId
        {
            get => _service.PupilId;
            set => _service.PupilId = value;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(IPupilDataViewModel.PUPIL_ID_PARAMETER, out object? value) && value is int pupilId)
            {
                PupilId = pupilId;
            }
        }

        protected override void AddAddNewParameters(Dictionary<string, object> parameters)
        {
            if (PupilId is not null)
            {
                parameters[IPupilDataViewModel.PUPIL_ID_PARAMETER] = PupilId;
            }
        }
    }
}