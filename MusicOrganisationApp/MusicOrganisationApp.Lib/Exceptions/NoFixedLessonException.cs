using MusicOrganisationApp.Lib.Models;

namespace MusicOrganisationApp.Lib.Exceptions
{
    public class NoFixedLessonException : Exception
    {
        public NoFixedLessonException(Pupil pupil) : base($"Pupil Id: {pupil.Id} Name: {pupil.Name} does not have a fixed lesson slot") { }
    }
}