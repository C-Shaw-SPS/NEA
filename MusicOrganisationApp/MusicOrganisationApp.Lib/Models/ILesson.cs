using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Models
{
    public interface ILesson
    {
        public TimeSpan StartTime { get; }

        public TimeSpan EndTime { get; }
    }
}