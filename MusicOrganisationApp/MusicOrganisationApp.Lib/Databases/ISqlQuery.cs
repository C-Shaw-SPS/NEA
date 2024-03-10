namespace MusicOrganisationApp.Lib.Databases
{
    public interface ISqlQuery : ISqlStatement
    {
        public abstract IEnumerable<Type> Tables { get; }
    }
}