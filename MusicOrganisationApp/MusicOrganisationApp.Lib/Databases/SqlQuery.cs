using System.Text;

namespace MusicOrganisationApp.Lib.Databases
{
    public abstract class SqlQuery
    {
        private const string _WHERE = "WHERE";
        private const string _AND = "AND";
        private const string _OR = "OR";
        private const string _IS_NOT = "IS NOT";
        private const string _EQUALS = "=";
        private const string _LIKE = "LIKE";
        private const string _GREATER = ">";
        private const string _LESS = "<";
        private const string _GREATER_OR_EQUAL = ">=";
        private const string _LESS_OR_EQUAL = "<=";
        private const string _NOT_EQUAL = "!=";
        private const string _ASC = "ASC";
        private const string _DESC = "DESC";
        private const string _INNER_JOIN = "INNER JOIN";
        private const string _LEFT_JOIN = "LEFT JOIN";

        private readonly string _tableName;
        private bool _selectAll;
        private readonly List<(string table, string column, string alias)> _columns;
        private readonly List<(string joinType, string newTable, string newColumn, string existingTable, string existingColumn)> _joins;
        private readonly List<(string keyword, string table, string column, string value, string comparison)> _conditions;
        private readonly List<(string column, string order)> _orderBys;
        private readonly int? _limit;

        private readonly HashSet<Type> _tables;

        public SqlQuery(string tableName, int? limit, Type tableType)
        {
            _tableName = tableName;
            _selectAll = false;
            _columns = [];
            _joins = [];
            _conditions = [];
            _orderBys = [];
            _limit = limit;
            _tables = [tableType];
        }

        public SqlQuery(string tableName, Type tableType) : this(tableName, null, tableType) { }

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

        public void AddInnerJoin<TNew, TExisting>(string newColumn, string existingColumn) where TNew : ITable where TExisting : ITable
        {
            AddJoin<TNew, TExisting>(_INNER_JOIN, newColumn, existingColumn);
        }

        public void AddLeftJoin<TNew, TExisting>(string newColumn, string existingColumn) where TNew : ITable where TExisting : ITable
        {
            AddJoin<TNew, TExisting>(_LEFT_JOIN, newColumn, existingColumn);
        }

        private void AddJoin<TNew, TExisting>(string joinType, string newColumn, string existingColumn) where TNew : ITable where TExisting : ITable
        {
            _joins.Add((joinType, TNew.TableName, newColumn, TExisting.TableName, existingColumn));
        }

        #region Conditions

        public void AddWhereEquals<TTable>(string column, object? value) where TTable : ITable
        {
            AddCondition<TTable>(_WHERE, column, value, _EQUALS);
        }

        public void AddWhereLike<TTable>(string column, string value) where TTable : ITable
        {
            AddCondition<TTable>(_WHERE, column, $"%{value}%", _LIKE);
        }

        public void AddAndEqual<TTable>(string column, object? value) where TTable : ITable
        {
            AddCondition<TTable>(_AND, column, value, _EQUALS);
        }

        public void AddOrEqual<TTable>(string column, object? value) where TTable: ITable
        {
            AddCondition<TTable>(_OR, column, value, _EQUALS);
        }

        public void AddAndGreaterThan<TTable>(string column, object? value) where TTable : ITable
        {
            AddCondition<TTable>(_AND, column, value, _GREATER);
        }

        public void AddAndGreaterOrEqual<TTable>(string column, object? value) where TTable : ITable
        {
            AddCondition<TTable>(_AND, column, value, _GREATER_OR_EQUAL);
        }

        public void AddAndLessThan<TTable>(string column, object value) where TTable : ITable
        {
            AddCondition<TTable>(_AND, column, value, _LESS);
        }

        public void AddAndLessOrEqual<TTable>(string column, object? value) where TTable : ITable
        {
            AddCondition<TTable>(_AND, column, value, _LESS_OR_EQUAL);
        }

        public void AddAndNotEqual<TTable>(string column, object? value) where TTable : ITable
        {
            string condition = value is null ? _IS_NOT : _NOT_EQUAL;
            AddCondition<TTable>(_AND, column, value, condition);

        }

        private void AddCondition<TTable>(string keyword, string column, object? value, string condition) where TTable : ITable
        {
            _conditions.Add((keyword, TTable.TableName, column, SqlFormatting.FormatSqlValue(value), condition));
        }

        #endregion

        public void AddOrderByAscending(string column)
        {
            _orderBys.Add((column, _ASC));
        }

        public void AddOrderByDescending(string column)
        {
            _orderBys.Add((column, _DESC));
        }

        #region SQL

        public string GetSql()
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

    public class SqlQuery<T> : SqlQuery where T : class, ITable, new()
    {
        public SqlQuery(int limit) : base(T.TableName, limit, typeof(T)) { }

        public SqlQuery() : base(T.TableName, typeof(T)) { }
    }
}