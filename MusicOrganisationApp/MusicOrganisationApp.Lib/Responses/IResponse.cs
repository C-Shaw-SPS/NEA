namespace MusicOrganisationApp.Lib.Responses
{
    internal interface IResponse<T>
    {
        public IEnumerable<T> Values { get; set; }
    }
}