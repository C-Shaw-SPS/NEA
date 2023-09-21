namespace OpenOpusDatabase.Lib.Databases
{
    public interface ISqlStorable
    {
        public int Id { get; set; }

        public string GetSqlValues();
    }
}