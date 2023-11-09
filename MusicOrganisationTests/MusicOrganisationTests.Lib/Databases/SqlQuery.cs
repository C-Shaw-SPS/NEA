
using System.Text;

namespace MusicOrganisationTests.Lib.Databases
{
    public class SqlQuery<T> where T : ITable
    {
        private List<(string table, string column, string alias)> _columns;
        private List<(string joinTable, string joinColumn, string tableColumn)> _joins;
        private List<(string table, string column, string value)> _equals;

        public SqlQuery()
        {
            _columns = new();
            _joins = new();
            _equals = new();
        }

        public void AddColumn<TTable>(string column, string alias) where TTable : ITable
        {
            _columns.Add((T.TableName, column, alias));
        }

        public void AddJoin<TTable>(string joinColumn, string tableColumn) where TTable : ITable
        {
            _joins.Add((TTable.TableName, joinColumn, tableColumn));
        }

        public void AddWhereEquals<TTable>(string column, string value) where TTable : ITable
        {
            _equals.Add((TTable.TableName, column, value));
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            AddSelectColumns(stringBuilder);
            AddJoins(stringBuilder);
            AddWhereEquals(stringBuilder);
            return stringBuilder.ToString();
        }

        private void AddSelectColumns(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("SELECT");

            foreach ((string table, string column, string alias) in _columns)
            {
                stringBuilder.AppendLine($"{table}.{column} AS {alias}");
            }

            stringBuilder.AppendLine($"FROM {T.TableName}");
        }

        private void AddJoins(StringBuilder stringBuilder)
        {
            foreach ((string joinTable, string joinColumn, string tableColumn) in _joins)
            {
                stringBuilder.AppendLine($"JOIN {joinTable} on {joinTable}.{joinColumn} = {T.TableName}.{tableColumn}");
            }
        }

        private void AddWhereEquals(StringBuilder stringBuilder)
        {
            if (_equals.Count > 0)
            {
                (string table, string column, string value) = _equals[0];
                stringBuilder.AppendLine($"WHERE {table}.{column} = {value}");
            }

            foreach ((string table, string column, string value) in _equals.Skip(1))
            {
                stringBuilder.AppendLine($"AND {table}.{column} = {value}");
            }
        }
    }
}