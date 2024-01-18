using System.Text;

namespace MusicOrganisation.Lib.Databases
{
    internal class UpdateStatement<T> : ISqlStatement where T : ITable
    {
        private readonly T _value;
        private readonly List<string> _columnsToUpdate;

        public UpdateStatement(T value)
        {
            _value = value;
            _columnsToUpdate = [];
        }

        public void AddColumnToUpdate(string columnName)
        {
            _columnsToUpdate.Add(columnName);
        }

        public void SetUpdateAll()
        {
            _columnsToUpdate.Clear();
            _columnsToUpdate.AddRange(T.GetColumnNames());
        }

        public string GetSql()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"UPDATE {T.TableName}");
            stringBuilder.AppendLine("SET");
            IDictionary<string, string> sqlValues = _value.GetSqlValues();
            List<string> sets = new();
            foreach (string column in _columnsToUpdate)
            {
                sets.Add($"{column} = {sqlValues[column]}");
            }
            stringBuilder.AppendLine(string.Join(",\n", sets));
            stringBuilder.AppendLine($"WHERE {nameof(ITable.Id)} = {_value.Id}");
            string statement = stringBuilder.ToString();
            return statement;
        }
    }
}