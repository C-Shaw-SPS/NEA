namespace MusicOrganisationTests.Lib.Databases
{
    public interface ITable : IIdentifiable
    {
        public abstract static string TableName { get; }

        public IEnumerable<string> GetSqlValues();

        public abstract static IEnumerable<string> GetColumnNames();
    }
}