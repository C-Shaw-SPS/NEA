﻿namespace MusicOrganisation.Lib.Databases
{
    public interface ITable
    {
        public abstract int Id { get; set; }

        public abstract bool IsDeleted { get; set; }

        public abstract static string TableName { get; }

        public IEnumerable<string> GetSqlValues();

        public abstract static IEnumerable<string> GetColumnNames();
    }
}