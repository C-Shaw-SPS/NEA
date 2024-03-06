using System.Text;

namespace MusicOrganisationApp.Lib.Databases
{
    internal class InsertStatement<T> : ISqlExecutable<T> where T : class, ITable, new()
    {
        private readonly StringBuilder _stringBuilder;
        private readonly IEnumerable<string> _fields;
        private bool _containsValues;

        public InsertStatement()
        {
            _stringBuilder = new();
            _fields = T.GetFieldNames();
            _containsValues = false;
        }

        public void AddValue(T value)
        {
            if (_containsValues)
            {
                _stringBuilder.AppendLine(SqlFormatting.COMMA_SEPARATOR);
            }
            else
            {
                AddInsertLine();
                _containsValues = true;
            }
            IEnumerable<string> sqlValues = GetSqlValues(value);
            _stringBuilder.Append(sqlValues.CommaJoin().AddBrackets());
        }

        private void AddInsertLine()
        {
            _stringBuilder.AppendLine($"INSERT INTO {T.TableName} {_fields.CommaJoin().AddBrackets()} VALUES");
        }

        private List<string> GetSqlValues(T value)
        {
            IDictionary<string, string> sqlValues = value.GetSqlValues();
            List<string> sqlStrings = [];
            foreach (string field in _fields)
            {
                sqlStrings.Add(sqlValues[field]);
            }
            return sqlStrings;
        }

        public string GetSql()
        {
            return _stringBuilder.ToString();
        }
    }
}