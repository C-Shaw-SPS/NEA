using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public class AllRepertoireViewModel : SearchableCollectionViewModel<Repertoire, RepertoireViewModel, EditRepertoireViewModel>, IQueryAttributable, IViewModel
    {
        private const string _ROUTE = nameof(AllRepertoireViewModel);

        public const string PUPIL_ID_PARAMETER = nameof(PUPIL_ID_PARAMETER);

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

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(PUPIL_ID_PARAMETER, out object? value) && value is int pupilId)
            {
                _service.PupilId = pupilId;
                await RefreshAsync();
            }
        }

        public override async Task RefreshAsync()
        {
            if (_service.PupilId is not null)
            {
                await base.RefreshAsync();
            }
        }

        protected override void AddAddNewParameters(Dictionary<string, object> parameters)
        {
            if (_service.PupilId is not null)
            {
                parameters[EditRepertoireViewModel.PUPIL_ID_PARAMETER] = _service.PupilId;
            }
        }
    }
}