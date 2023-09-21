namespace OpenOpusDatabase.Lib.Responses
{
    public interface IResponse<T>
    {
        public List<T> Values { get; set; }
    }
}