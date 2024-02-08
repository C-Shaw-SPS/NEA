using MusicOrganisationApp.Lib.Services;

namespace MusicOrganisationApp.Lib.ViewModels
{
    public class StartupViewModel : ViewModelBase, IViewModel
    {
        private const string _ROUTE = nameof(StartupViewModel);

        private readonly StartupService _startupService;

        public StartupViewModel()
        {
            _startupService = new(_database);  
        }

        public static string Route => _ROUTE;

        public async Task Init()
        {
            await _startupService.InitialiseComposersAndWorks();
        }
    }
}
