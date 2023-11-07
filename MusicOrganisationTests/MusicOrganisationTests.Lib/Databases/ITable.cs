namespace MusicOrganisationTests.Lib.Databases
{
    public interface ITable
    {
        public abstract int Id { get; set; }

        public abstract static string TableName { get; }

        public IEnumerable<string> GetSqlValues();

        public abstract static IEnumerable<string> GetColumnNames();

        public static IEnumerable<string> GetColumnNamesWithTableName<T>() where T : ITable
        {
            return T.GetColumnNames().Select(x => T.TableName + "." + x);
        }
    }
}