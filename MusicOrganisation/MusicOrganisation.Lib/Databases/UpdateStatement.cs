using System.Text;

namespace MusicOrganisation.Lib.Databases
{
    public class UpdateStatement : ISqlStatement
    {
        private readonly string _tableName;
        private readonly int _id;
        private readonly IDictionary<string, string> _sqlValues;
        private readonly IList<string> _columnsToUpdate;

        public UpdateStatement(string tableName, int id)
        {
            _tableName = tableName;
            _id = id;
            _sqlValues = new Dictionary<string, string>();
            _columnsToUpdate = [];
        }

        public void AddColumnToUpdate(string columnName, object? value)
        {
            string sqlValue = SqlFormatting.FormatValue(value);
            _sqlValues[columnName] = sqlValue;
        }

        public string GetSql()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"UPDATE {_tableName}");
            stringBuilder.AppendLine("SET");
            List<string> sets = new();
            foreach (string column in _sqlValues.Keys)
            {
                sets.Add($"{column} = {_sqlValues[column]}");
            }
            stringBuilder.AppendLine(string.Join(",\n", sets));
            stringBuilder.AppendLine($"WHERE {nameof(ITable.Id)} = {_id}");
            string statement = stringBuilder.ToString();
            return statement;
        }

        public static UpdateStatement GetUpdateAllColumns<T>(T value) where T : ITable
        {
            UpdateStatement updateStatement = new(T.TableName, value.Id);
            IDictionary<string, string> sqlValues = value.GetSqlValues();
            foreach (string column in sqlValues.Keys)
            {
                updateStatement.AddColumnToUpdate(column, sqlValues[column]);
            }
            return updateStatement;
        }
    }

    public class UpdateStatement<T> : UpdateStatement where T : ITable
    {
        public UpdateStatement(int id) : base(T.TableName, id) { }
    }
}