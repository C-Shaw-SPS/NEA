using System.Text;

namespace MusicOrganisationTests.Lib.Databases
{
    internal class InsertCommand<T> where T : ISqlStorable, new()
    {
        private StringBuilder _stringBuilder;
        private IEnumerable<string> _columns;
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
            _stringBuilder.AppendLine($"INSERT INTO {T.TableName} {_columns.CommaJoin()} VALUES");
        }

        public void AddValue(T value)
        {
            if (_containsValues)
            {
                _stringBuilder.Append(", ");
            }
            else
            {
                _containsValues = true;
            }
            _stringBuilder.Append(value.GetSqlValues().CommaJoin());
        }

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }
    }
}
