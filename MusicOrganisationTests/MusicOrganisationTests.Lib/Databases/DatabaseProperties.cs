﻿using SQLite;

namespace MusicOrganisationTests.Lib.Databases
{
    public static class DatabaseProperties
    {
        public const string NAME = "MusicOrganisation.db";

        internal const SQLiteOpenFlags FLAGS =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;
    }
}