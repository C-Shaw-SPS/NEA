namespace MusicOrganisationApp.Lib.Exceptions
{
    public class DayOfWeekException : Exception
    {
        public DayOfWeekException(DayOfWeek dayOfWeek) : base($"No such day of week '{dayOfWeek}'") { }
    }
}
