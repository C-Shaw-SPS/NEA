using System.Text;

namespace MusicOrganisationApp.Lib.Databases
{
    public class SqlQuery<T> : ConditionalExecutable<T>, ISqlQuery where T : class, ITable, new()
    {
        private readonly string _tableName = T.TableName;
        private bool _selectAll;
        private readonly List<(string table, string column, string alias)> _columns = [];
        private readonly List<(string joinType, string newTable, string newColumn, string existingTable, string existingColumn)> _joins = [];
        private readonly List<(string column, string order)> _orderBys = [];
        private readonly int? _limit;

        private readonly HashSet<Type> _tables = [typeof(T)];

        public SqlQuery(int? limit)
        {
            _selectAll = false;
            _limit = limit;
        }

        public SqlQuery() : this(null) { }

        public string TableName => _tableName;

        public bool SelectAll
        {
            get => _selectAll;
            set => _selectAll = value;
        }

        public IEnumerable<Type> Tables => _tables;

        public void AddColumn<TTable>(string column, string alias) where TTable : ITable
        {
            _columns.Add((TTable.TableName, column, alias));
        }

        public void AddColumn<TTable>(string column) where TTable : ITable
        {
            AddColumn<TTable>(column, column);
        }

        #region Joins

        public void AddInnerJoin<TNew, TExisting>(string newColumn, string existingColumn) where TNew : ITable where TExisting : ITable
        {
            AddJoin<TNew, TExisting>(ISqlExecutable.INNER_JOIN, newColumn, existingColumn);
        }

        private void AddJoin<TNew, TExisting>(string joinType, string newColumn, string existingColumn) where TNew : ITable where TExisting : ITable
        {
            _tables.Add(typeof(TNew));
            _joins.Add((joinType, TNew.TableName, newColumn, TExisting.TableName, existingColumn));
        }

        #endregion

        public void AddOrderByAscending(string column)
        {
            _orderBys.Add((column, ISqlExecutable.ASC));
        }

        public void AddOrderByDescending(string column)
        {
            _orderBys.Add((column, ISqlExecutable.DESC));
        }

        #region SQL

        public override string GetSql()
        {
            StringBuilder stringBuilder = new();
            AddSelectColumnsToStringbuilder(stringBuilder);
            AddJoinsToStringbuilder(stringBuilder);
            AddConditionsToStringBuilder(stringBuilder);
            AddOrderBys(stringBuilder);
            AddLimit(stringBuilder);
            string sql = stringBuilder.ToString();
            return sql;
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
            foreach ((string joinType, string newTable, string newColumn, string existingTable, string existingColumn) in _joins)
            {
                stringBuilder.AppendLine($"{joinType} {newTable} ON {newTable}.{newColumn} = {existingTable}.{existingColumn}");
            }
        }

        private void AddConditionsToStringBuilder(StringBuilder stringBuilder)
        {
            for (int i = 0; i < _conditions.Count; ++i)
            {
                string condition = FormatCondition(i);
                stringBuilder.AppendLine(condition);
            }
        }

        private string FormatCondition(int conditionIndex)
        {
            (string keyword, string table, string column, string value, string comparison) = _conditions[conditionIndex];
            return $"{keyword} {table}.{column} {comparison} {value}";
        }

        private void AddOrderBys(StringBuilder stringBuilder)
        {
            if (_orderBys.Count > 0)
            {
                IEnumerable<string> formattedOrderBys = GetFormattedOrderBys();
                stringBuilder.AppendLine("ORDER BY");
                stringBuilder.AppendLine(string.Join(", ", formattedOrderBys));
            }
        }

        private IEnumerable<string> GetFormattedOrderBys()
        {
            IEnumerable<string> formattedOrderBys =
                from orderBy in _orderBys
                select $"{orderBy.column} {orderBy.order}";
            return formattedOrderBys;
        }

        private void AddLimit(StringBuilder stringBuilder)
        {
            if (_limit is not null)
            {
                stringBuilder.AppendLine($"LIMIT {_limit}");
            }
        }

        #endregion
    }
}