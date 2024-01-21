using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.Databases
{
    public class DeleteQuery<T> : ISqlQuery where T : class, ITable, new()
    {
        private readonly List<(string column, string value)> _conditions;
        
        public DeleteQuery()
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
