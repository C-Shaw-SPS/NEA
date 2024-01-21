namespace MusicOrganisationApp.Lib
{
    public interface ITable
    {
        public abstract static string TableName { get; }

        public abstract int Id { get; set; }

        public static abstract IEnumerable<string> GetColumnNames();

        public abstract IDictionary<string, string> GetSqlValues();
    }
}