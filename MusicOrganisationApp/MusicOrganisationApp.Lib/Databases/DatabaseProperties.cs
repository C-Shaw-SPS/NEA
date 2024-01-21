using SQLite;

namespace MusicOrganisationApp.Lib.Databases
{
    public static class DatabaseProperties
    {
        public const string NAME = "MusicOrganisationApp.db";

        internal const SQLiteOpenFlags FLAGS =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;
    }
}