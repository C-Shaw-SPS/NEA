using System.Text;

namespace MusicOrganisationApp.Lib.Databases
{
    public class DeleteStatement<T> where T : class, ITable, new()
    {
        private readonly List<(string column, string value)> _conditions;

        public DeleteStatement()
        {
            _conditions = [];
        }

        public string TableName => T.TableName;

        public void AddCondition(string column, object? value)
        {
            _conditions.Add((column, value.FormatSqlValue()));
        }

        public string GetSql()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"DELETE FROM {T.TableName} WHERE");
            stringBuilder.AppendLine(GetConditions());
            string sql = stringBuilder.ToString();
            return sql;
        }

        private string GetConditions()
        {
            IEnumerable<string> conditions = _conditions.Select(condition => FormatCondition(condition.column, condition.value));
            return string.Join("\nOR", conditions);
        }

        private static string FormatCondition(string column, string value)
        {
            return $"{column} = {value}";
        }
    }
}
