using System.Text;

namespace MusicOrganisationTests.Lib.Databases
{
    public class SqlQuery<T> where T : ITable
    {
        private List<(string table, string column, string alias)> _columns;
        private List<(string newTable, string newColumn, string existingTable, string existingColumn)> _joins;
        private List<(string table, string column, string value, string operation)> _conditions;

        public SqlQuery()
        {
            _columns = new();
            _joins = new();
            _conditions = new();
        }

        public void AddColumn<TTable>(string column, string alias) where TTable : ITable
        {
            _columns.Add((TTable.TableName, column, alias));
        }

        public void AddJoin<TNew, TExisting>(string newColumn, string existingColumn) where TNew : ITable where TExisting : ITable
        {
            _joins.Add((TNew.TableName, newColumn, TExisting.TableName, existingColumn));
        }

        public void AddWhereEquals<TTable>(string column, object? value) where TTable : ITable
        {
            _conditions.Add((TTable.TableName, column, SqlFormatting.FormatValue(value), "="));
        }

        public void AddWhereLike<TTable>(string column, string value) where TTable : ITable
        {
            _conditions.Add((TTable.TableName, column, SqlFormatting.FormatLikeString(value), "LIKE"));
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            AddSelectColumnsToStringbuilder(stringBuilder);
            AddJoinsToStringbuilder(stringBuilder);
            AddWhereEqualsToStringbuilder(stringBuilder);
            return stringBuilder.ToString();
        }

        private void AddSelectColumnsToStringbuilder(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("SELECT");

            IEnumerable<string> formattedColumns = _columns.Select(c => $"{c.table}.{c.column} AS {c.alias}");
            stringBuilder.AppendLine(string.Join(",\n", formattedColumns));

            stringBuilder.AppendLine($"FROM {T.TableName}");
        }

        private void AddJoinsToStringbuilder(StringBuilder stringBuilder)
        {
            foreach ((string newTable, string newColumn, string existingTable, string existingColumn) in _joins)
            {
                stringBuilder.AppendLine($"JOIN {newTable} ON {newTable}.{newColumn} = {existingTable}.{existingColumn}");
            }
        }

        private void AddWhereEqualsToStringbuilder(StringBuilder stringBuilder)
        {
            if (_conditions.Count > 0 )
            {
                stringBuilder.AppendLine("WHERE");
                IEnumerable<string> formattedEquals = _conditions.Select(e => $"{e.table}.{e.column} {e.operation} {e.value}");
                stringBuilder.AppendLine(string.Join("\nAND", formattedEquals));
            }
        }
    }
}