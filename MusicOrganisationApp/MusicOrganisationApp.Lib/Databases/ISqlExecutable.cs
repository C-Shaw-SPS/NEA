namespace MusicOrganisationApp.Lib.Databases
{
    public interface ISqlExecutable<T> where T : class, ITable, new()
    {
        public string GetSql();
    }
}