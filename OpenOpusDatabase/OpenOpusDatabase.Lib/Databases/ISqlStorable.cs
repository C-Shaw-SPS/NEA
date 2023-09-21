namespace OpenOpusDatabase.Lib.Databases
{
    public interface ISqlStorable
    {
        public abstract int Id { get; set; }

        public abstract static string TableName { get; }

        public string GetSqlValues();

        public string GetSqlColumnNames();
    }
}