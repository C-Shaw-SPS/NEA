namespace OpenOpusDatabase.Lib.Databases
{
    public interface ISqlStorable
    {
        public abstract int Id { get; set; }

        public abstract static string TableName { get; }

        public List<string> GetSqlValues();

        public abstract static List<string> GetColumnNames();
    }
}