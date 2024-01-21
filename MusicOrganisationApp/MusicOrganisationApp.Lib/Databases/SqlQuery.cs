using System.Text;

namespace MusicOrganisationApp.Lib.Databases
{
    public abstract class SqlQuery : ISqlStatement
    {
        public const int DEFAULT_LIMIT = 256;

        private readonly string _tableName;
        private bool _selectAll;
        private readonly List<(string table, string column, string alias)> _columns;
        private readonly List<(string newTable, string newColumn, string existingTable, string existingColumn)> _joins;
        private readonly List<(string table, string column, string value, string comparison)> _conditions;
        private readonly List<(string table, string column)> _orderBys;
        private readonly int? _limit;

        public SqlQuery(string tableName, int? limit)
        {
            _tableName = tableName;
            _selectAll = false;
            _columns = [];
            _joins = [];
            _conditions = [];
            _orderBys = [];
            _limit = limit;
        }

        public SqlQuery(string tableName) : this(tableName, null) { }

        public string TableName => _tableName;

        public void SetSelectAll()
        {
            _selectAll = true;
        }

        public void AddColumn<TTable>(string column, string alias) where TTable : ITable
        {
            _columns.Add((TTable.TableName, column, alias));
        }

        public void AddColumn<TTable>(string column) where TTable : ITable
        {
            AddColumn<TTable>(column, column);
        }

        public void AddJoin<TNew, TExisting>(string newColumn, string existingColumn) where TNew : ITable where TExisting : ITable
        {
            _joins.Add((TNew.TableName, newColumn, TExisting.TableName, existingColumn));
        }

        public void AddWhereEquals<TTable>(string column, object? value) where TTable : ITable
        {
            _conditions.Add((TTable.TableName, column, SqlFormatting.FormatSqlValue(value), "="));
        }

        public void AddWhereLike<TTable>(string column, string value) where TTable : ITable
        {
            _conditions.Add((TTable.TableName, column, SqlFormatting.FormatLikeString(value), "LIKE"));
        }

        public void AddOrderBy<TTable>(string column) where TTable : ITable
        {
            _orderBys.Add((TTable.TableName, column));
        }

        public string GetSql()
        {
            StringBuilder stringBuilder = new();
            AddSelectColumnsToStringbuilder(stringBuilder);
            AddJoinsToStringbuilder(stringBuilder);
            AddWhereEqualsToStringbuilder(stringBuilder);
            AddOrderBys(stringBuilder);
            AddLimit(stringBuilder);

            return stringBuilder.ToString();
        }

        private void AddSelectColumnsToStringbuilder(StringBuilder stringBuilder)
        {
            if (_selectAll)
            {
                stringBuilder.AppendLine($"SELECT * FROM {_tableName}");
            }
            else
            {
                stringBuilder.AppendLine("SELECT");

                IEnumerable<string> formattedColumns = _columns.Select(c => $"{c.table}.{c.column} AS {c.alias}");
                stringBuilder.AppendLine(string.Join(",\n", formattedColumns));

                stringBuilder.AppendLine($"FROM {_tableName}");
            }
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
            if (_conditions.Count >= 1)
            {
                stringBuilder.AppendLine("WHERE");
                stringBuilder.AppendLine(FormatCondition(0));
            }
            for (int i = 2; i < _conditions.Count; i++)
            {
                string condition = FormatCondition(i);
                string line = $"AND {condition}";
                stringBuilder.AppendLine(line);
            }
        }

        private string FormatCondition(int conditionIndex)
        {
            (string table, string column, string value, string comparison) = _conditions[conditionIndex];
            return $"{table}.{column} {comparison} {value}";
        }

        private void AddOrderBys(StringBuilder stringBuilder)
        {
            if (_orderBys.Count > 0)
            {
                stringBuilder.AppendLine("ORDER BY");
                IEnumerable<string> formattedOrderBys = _orderBys.Select(s => $"{s.table}.{s.column}");
                stringBuilder.AppendLine(string.Join(", ", formattedOrderBys));
            }
        }

        private void AddLimit(StringBuilder stringBuilder)
        {
            if (_limit is not null)
            {
                stringBuilder.AppendLine($"LIMIT {_limit}");
            }
        }
    }

    public class SqlQuery<T> : SqlQuery where T : class, ITable, new()
    {

        public SqlQuery(int limit) : base(T.TableName, limit) { }

        public SqlQuery() : base(T.TableName) { }
    }
}