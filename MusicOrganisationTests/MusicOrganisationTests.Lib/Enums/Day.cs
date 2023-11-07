namespace MusicOrganisationTests.Lib.Enums
{
    public enum Day
    {
        Monday = 0b0000001,
        Tuesday = 0b0000010,
        Wednesday = 0b0000100,
        Thursday = 0b0001000,
        Friday = 0b0010000,
        Saturday = 0b0100000,
        Sunday = 0b1000000
    }

    public static class DayConverter
    {
        public static Day ToDay(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                DayOfWeek.Monday => Day.Monday,
                DayOfWeek.Tuesday => Day.Tuesday,
                DayOfWeek.Wednesday => Day.Wednesday,
                DayOfWeek.Thursday => Day.Thursday,
                DayOfWeek.Friday => Day.Friday,
                DayOfWeek.Saturday => Day.Saturday,
                DayOfWeek.Sunday => Day.Sunday,
                _ => throw new Exception($"Invalid day of week: {dayOfWeek}"),
            };
        }
    }
}