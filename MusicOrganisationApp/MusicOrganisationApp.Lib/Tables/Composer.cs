using SQLite;

namespace MusicOrganisationApp.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class Composer : ITable
    {
        private const string _TABLE_NAME = $"{nameof(Composer)}s";

        public static string TableName => _TABLE_NAME;


        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public static IEnumerable<string> GetColumnNames()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string> GetSqlValues()
        {
            throw new NotImplementedException();
        }
    }
}
