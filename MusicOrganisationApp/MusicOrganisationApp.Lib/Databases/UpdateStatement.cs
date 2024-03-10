using System.Text;

namespace MusicOrganisationApp.Lib.Databases
{
    public class UpdateStatement<T> : ISqlStatement<T> where T : class, ITable, new()
    {
        private readonly T _value;
        private readonly IDictionary<string, string> _sqlValues;

        public UpdateStatement(T value)
        {
            _value = value;
            _sqlValues = _value.GetSqlValues();
        }

        public string GetSql()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"UPDATE {T.TableName}");
            stringBuilder.AppendLine("SET");
            List<string> setValues = [];
            foreach (string field in _sqlValues.Keys)
            {
                string value = _sqlValues[field];
                string setValue = $"{field} = {value}";
                setValues.Add(setValue);
            }
            stringBuilder.AppendLine(string.Join(",\n", setValues));
            stringBuilder.AppendLine($"WHERE {nameof(ITable.Id)} = {_value.Id}");
            string statement = stringBuilder.ToString();
            return statement;
        }
    }
}