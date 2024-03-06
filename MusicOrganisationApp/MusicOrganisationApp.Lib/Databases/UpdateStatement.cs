using System.Text;

namespace MusicOrganisationApp.Lib.Databases
{
    public class UpdateStatement<T> : ISqlExecutable<T> where T : class, ITable, new()
    {
        private readonly string _tableName;
        private readonly int _id;
        private readonly IDictionary<string, string> _sqlValues;
        private readonly IList<string> _fieldsToUpdate;

        public UpdateStatement(int id)
        {
            _tableName = T.TableName;
            _id = id;
            _sqlValues = new Dictionary<string, string>();
            _fieldsToUpdate = [];
        }

        public string TableName => _tableName;

        public void AddFieldsToUpdate(string fieldName, object? value)
        {
            string sqlValue = SqlFormatting.FormatSqlValue(value);
            _sqlValues[fieldName] = sqlValue;
        }

        public string GetSql()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"UPDATE {_tableName}");
            stringBuilder.AppendLine("SET");
            List<string> setValues = [];
            foreach (string field in _sqlValues.Keys)
            {
                string value = _sqlValues[field];
                string setValue = $"{field} = {value}";
                setValues.Add(setValue);
            }
            stringBuilder.AppendLine(string.Join(",\n", setValues));
            stringBuilder.AppendLine($"WHERE {nameof(ITable.Id)} = {_id}");
            string statement = stringBuilder.ToString();
            return statement;
        }

        public static UpdateStatement<T> GetUpdateAllFields(T value)
        {
            UpdateStatement<T> updateStatement = new(value.Id);
            IDictionary<string, string> sqlValues = value.GetSqlValues();
            foreach (string field in sqlValues.Keys)
            {
                updateStatement._sqlValues[field] = sqlValues[field];
            }
            return updateStatement;
        }
    }
}