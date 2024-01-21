using System.Text;

namespace MusicOrganisationApp.Lib.Databases
{
    internal class InsertStatement<T> : ISqlStatement where T : ITable, new()
    {
        private readonly StringBuilder _stringBuilder;
        private readonly IEnumerable<string> _columns;
        private bool _containsValues;

        public InsertStatement()
        {
            _stringBuilder = new();
            _columns = T.GetColumnNames();
            _containsValues = false;
        }

        public string TableName => T.TableName;

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
            _stringBuilder.AppendLine($"INSERT INTO {T.TableName} {_columns.CommaJoin().AddBrackets()} VALUES");
        }

        private List<string> GetSqlValues(T value)
        {
            IDictionary<string, string> sqlValues = value.GetSqlValues();
            List<string> sqlStrings = new();
            foreach (string column in _columns)
            {
                sqlStrings.Add(sqlValues[column]);
            }
            return sqlStrings;
        }

        public string GetSql()
        {
            return _stringBuilder.ToString();
        }
    }
}