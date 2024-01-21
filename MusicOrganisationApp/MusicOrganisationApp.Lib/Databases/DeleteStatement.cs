using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace MusicOrganisationApp.Lib.Databases
{
    public class DeleteStatement<T> : ISqlExecutable<T> where T : class, ITable, new()
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
            stringBuilder.AppendLine($"DELETE FROM {T.TableName}");

            IEnumerable<string> conditions = GetConditions();

            if (conditions.Any())
            {
                stringBuilder.AppendLine("WHERE");
                stringBuilder.AppendLine(string.Join("\nOR", conditions));
            }

            string sql = stringBuilder.ToString();
            return sql;
        }

        private IEnumerable<string> GetConditions()
        {
            IEnumerable<string> conditions = _conditions.Select(condition => FormatCondition(condition.column, condition.value));
            return conditions;
        }

        private static string FormatCondition(string column, string value)
        {
            return $"{column} = {value}";
        }
    }
}
