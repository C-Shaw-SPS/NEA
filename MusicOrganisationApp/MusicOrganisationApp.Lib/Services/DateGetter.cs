namespace MusicOrganisationApp.Lib.Services
{
    public static class DateGetter
    {
        public static DateTime FromDayOfWeek(DateTime dateInWeek, DayOfWeek dayOfWeek)
        {
            int dayDiff = dayOfWeek - dateInWeek.DayOfWeek;
            DateTime dayInWeek = dateInWeek.AddDays(dayDiff);
            return dayInWeek;
        }
    }
}