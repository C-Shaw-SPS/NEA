namespace MusicOrganisationApp.Lib.Responses
{
    public interface IResponse<T>
    {
        public IEnumerable<T> Values { get; set; }
    }
}