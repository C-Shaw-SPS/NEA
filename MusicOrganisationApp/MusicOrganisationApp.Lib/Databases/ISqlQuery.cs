namespace MusicOrganisationApp.Lib.Databases
{
    public interface ISqlQuery : ISqlExecutable
    {
        public abstract IEnumerable<Type> Tables { get; }
    }
}