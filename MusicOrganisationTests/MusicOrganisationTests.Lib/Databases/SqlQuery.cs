using System.Text;

namespace MusicOrganisationTests.Lib.Databases
{
    public class SqlQuery<T> where T : ITable
    {
        private List<(string table, string column, string alias)> _columns;
        private List<(string table1, string column1, string table2, string column2)> _joins;
        private List<(string table, string column, string value)> _equals;

        public SqlQuery()
        {
            _columns = new();
            _joins = new();
            _equals = new();
        }

        public void AddColumn<TTable>(string column, string alias) where TTable : ITable
        {
            _columns.Add((TTable.TableName, column, alias));
        }

        public void AddJoin<TTable1, TTable2>(string column1, string column2) where TTable1 : ITable where TTable2 : ITable
        {
            _joins.Add((TTable1.TableName, column1, TTable2.TableName, column2));
        }

        public void AddWhereEquals<TTable>(string column, object? value) where TTable : ITable
        {
            _equals.Add((TTable.TableName, column, SqlFormatting.FormatValue(value)));
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
            foreach ((string table1, string column1, string table2, string column2) in _joins)
            {
                stringBuilder.AppendLine($"JOIN {table1} ON {table1}.{column1} = {table2}.{column2}");
            }
        }

        private void AddWhereEqualsToStringbuilder(StringBuilder stringBuilder)
        {
            if (_equals.Count > 0 )
            {
                stringBuilder.AppendLine("WHERE");
                IEnumerable<string> formattedEquals = _equals.Select(e => $"{e.table}.{e.column} = {e.value}");
                stringBuilder.AppendLine(string.Join("\nAND", formattedEquals));
            }
        }
    }
}