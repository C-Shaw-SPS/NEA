namespace MusicOrganisationApp.Lib.Services
{
    public static class DictionaryGetter
    {
        public static Dictionary<int, T> GetDictionary<T>(this IEnumerable<T> values) where T : class, ITable, new()
        {
            Dictionary<int, T> dictionary = [];
            foreach (T value in values)
            {
                dictionary.Add(value.Id, value);
            }
            return dictionary;
        }
    }
}