namespace MusicOrganisationApp.Lib.ViewModels
{
    public interface IPupilDataViewModel : IViewModel
    {
        public const string PUPIL_ID_PARAMETER = nameof(PUPIL_ID_PARAMETER);

        public int? PupilId { get; set; }

        public void ApplyPupilAttribute(IDictionary<string, object> query)
        {
            if (query.TryGetValue(PUPIL_ID_PARAMETER, out object? value) && value is int pupilId)
            {
                PupilId = pupilId;
            }
        }
    }
}