namespace MusicOrganisationApp.Lib.Models
{
    public interface ILesson<T> : IIdentifiable where T : class, ITable, new()
    {
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}