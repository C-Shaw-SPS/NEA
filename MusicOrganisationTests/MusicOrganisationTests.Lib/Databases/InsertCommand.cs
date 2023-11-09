using System.Text;

namespace MusicOrganisationTests.Lib.Databases
{
    internal class InsertCommand<T> where T : ITable, new()
    {
        private readonly StringBuilder _stringBuilder;
        private readonly IEnumerable<string> _columns;
        private bool _containsValues;

        public InsertCommand()
        {
            _stringBuilder = new();
            _columns = T.GetColumnNames();
            _containsValues = false;
            AddInsertLine();
        }

        private void AddInsertLine()
        {
            _stringBuilder.AppendLine($"INSERT INTO {T.TableName} {_columns.CommaJoin().AddBrackets()} VALUES");
        }

        public void AddValue(T value)
        {
            if (_containsValues)
            {
                _stringBuilder.Append(SqlFormatting.COMMA_SEPARATOR);
            }
            else
            {
                _containsValues = true;
            }
            _stringBuilder.Append(value.GetSqlValues().CommaJoin().AddBrackets());
        }

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }
    }
}
