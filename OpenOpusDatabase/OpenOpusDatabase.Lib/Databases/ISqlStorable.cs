namespace OpenOpusDatabase.Lib.Databases
{
    public interface ISqlStorable
    {
        public abstract int Id { get; set; }

        public abstract static string TableName { get; }

        public IEnumerable<string> GetSqlValues();

        public abstract static IEnumerable<string> GetColumnNames();
    }
}