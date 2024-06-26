﻿using MusicOrganisationApp.Lib.Databases;

namespace MusicOrganisationApp.Lib
{
    public interface ITable : IIdentifiable
    {
        public abstract static string TableName { get; }

        public static abstract IEnumerable<string> GetFieldNames();

        public abstract IDictionary<string, string> GetSqlValues();
    }
}