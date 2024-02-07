using MusicOrganisationApp.Lib.Services;

namespace MusicOrganisationApp.Lib.ViewModels
{
    public class StartupViewModel : ViewModelBase
    {
        private readonly StartupService _startupService;

        public StartupViewModel()
        {
            _startupService = new(_database);  
        }

        public async Task Init()
        {
            await _startupService.InitialiseComposersAndWorks();
        }
    }
}
