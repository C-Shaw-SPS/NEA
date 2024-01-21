namespace MusicOrganisationApp.Lib.Exceptions
{
    public class NoMoreFlagsException : Exception
    {
        public NoMoreFlagsException(DayOfWeek dayOfWeek) : base($"No more flags avaliable on {dayOfWeek}") { }
    }
}