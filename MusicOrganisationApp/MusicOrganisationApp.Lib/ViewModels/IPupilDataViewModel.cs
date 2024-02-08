namespace MusicOrganisationApp.Lib.ViewModels
{
    public interface IPupilDataViewModel : IViewModel
    {
        public const string PUPIL_ID_PARAMETER = nameof(PUPIL_ID_PARAMETER);

        public int? PupilId { get; set; }
    }
}