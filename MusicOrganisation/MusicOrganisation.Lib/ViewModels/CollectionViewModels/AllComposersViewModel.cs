using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.ViewModels.EditViewModels;
using MusicOrganisation.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisation.Lib.ViewModels.CollectionViewModels
{
    public partial class AllComposersViewModel : CollectionViewModelBase<ComposerData>
    {
        private const int _LIMIT = 128;

        public const string ROUTE = nameof(AllComposersViewModel);

        private static readonly Dictionary<string, string> _orderings = new()
        {
            { "Name", nameof(ComposerData.Name) },
            { "Year of birth", nameof(ComposerData.BirthYear) },
            { "Year of death", nameof(ComposerData.DeathYear) }
        };

        private readonly ComposerService _composerService;



        public AllComposersViewModel() : base(_orderings)
        {
            _composerService = new(_databasePath);
        }

        public async Task RefreshAsync()
        {
            await SearchAsync();
        }

        protected override async Task SearchAsync()
        {
            string ordering = _orderings[Ordering];

            SqlQuery<ComposerData> query = new();
            query.SetSelectAll();
            query.AddWhereLike<ComposerData>(nameof(ComposerData.Name), SearchText, SqlBool.OR);
            query.AddOrderBy<ComposerData>(ordering);
            query.SetLimit(_LIMIT);

            string queryString = query.ToString();

            IEnumerable<ComposerData> composers = await _composerService.QueryAsync<ComposerData>(queryString);
            Collection.Clear();
            foreach (ComposerData compsoer in composers)
            {
                Collection.Add(compsoer);
            }
        }

        protected override async Task SelectAsync()
        {
            if (SelectedItem is not null)
            {
                Dictionary<string, object> parameters = new()
                {
                    [ComposerViewModel.COMPOSER_ID] = SelectedItem.Id
                };
                await GoToAsync(parameters, ComposerViewModel.ROUTE);
            }
        }

        protected override async Task AddNewAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [EditComposerViewModel.IS_NEW_PARAMETER] = true
            };
            await GoToAsync(parameters, EditComposerViewModel.ROUTE);
        }
    }
}