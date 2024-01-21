namespace MusicOrganisationApp.Lib.Databases
{
    public class DropTableStatement<T> : ISqlStatement where T : class, ITable, new()
    {
        private readonly string _sql = $"DROP TABLE {T.TableName}";

        public string GetSql()
        {
            return _sql;
        }
    }
}