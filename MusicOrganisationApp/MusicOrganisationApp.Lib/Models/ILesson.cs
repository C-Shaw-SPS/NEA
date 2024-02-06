namespace MusicOrganisationApp.Lib.Models
{
    public interface ILesson
    {
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}