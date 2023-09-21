using SQLite;

namespace OpenOpusDatabase.Lib.Databases
{
    public static class DatabaseProperties
    {
        public const string NAME = "OpenOpusDatabase.db";

        public const SQLiteOpenFlags FLAGS =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;
    }
}