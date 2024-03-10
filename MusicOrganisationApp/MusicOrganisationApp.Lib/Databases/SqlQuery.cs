using System.Text;

namespace MusicOrganisationApp.Lib.Databases
{
    public class SqlQuery<T> : ConditionalExecutable<T>, ISqlQuery where T : class, ITable, new()
    {
        private const string _JOIN = "JOIN";

        private readonly string _tableName = T.TableName;
        private bool _selectAll;
        private readonly List<(string table, string field, string alias)> _fields = [];
        private readonly List<(string newTable, string newField, string existingTable, string existingField)> _joins = [];
        private readonly List<(string field, string order)> _orderBys = [];
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

        public void AddField<TTable>(string field, string alias) where TTable : ITable
        {
            _fields.Add((TTable.TableName, field, alias));
        }

        public void AddField<TTable>(string field) where TTable : ITable
        {
            AddField<TTable>(field, field);
        }

        #region Joins

        public void AddInnerJoin<TNew, TExisting>(string newField, string existingField) where TNew : ITable where TExisting : ITable
        {
            _tables.Add(typeof(TNew));
            _joins.Add((TNew.TableName, newField, TExisting.TableName, existingField));
        }

        #endregion

        public void AddOrderByAscending(string field)
        {
            _orderBys.Add((field, ISqlStatement.ASC));
        }

        public void AddOrderByDescending(string field)
        {
            _orderBys.Add((field, ISqlStatement.DESC));
        }

        #region SQL

        public override string GetSql()
        {
            StringBuilder stringBuilder = new();
            AddSelectFieldsToStringbuilder(stringBuilder);
            AddJoinsToStringbuilder(stringBuilder);
            AddConditionsToStringBuilder(stringBuilder);
            AddOrderBys(stringBuilder);
            AddLimit(stringBuilder);
            string sql = stringBuilder.ToString();
            return sql;
        }

        private void AddSelectFieldsToStringbuilder(StringBuilder stringBuilder)
        {
            if (_selectAll)
            {
                stringBuilder.AppendLine($"SELECT * FROM {_tableName}");
            }
            else
            {
                stringBuilder.AppendLine("SELECT");

                IEnumerable<string> formattedFields = _fields.Select(c => $"{c.table}.{c.field} AS {c.alias}");
                stringBuilder.AppendLine(string.Join(",\n", formattedFields));

                stringBuilder.AppendLine($"FROM {_tableName}");
            }
        }

        private void AddJoinsToStringbuilder(StringBuilder stringBuilder)
        {
            foreach ((string newTable, string newField, string existingTable, string existingField) in _joins)
            {
                stringBuilder.AppendLine($"{_JOIN} {newTable} ON {newTable}.{newField} = {existingTable}.{existingField}");
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
            (string keyword, string table, string field, string value, string comparison) = _conditions[conditionIndex];
            return $"{keyword} {table}.{field} {comparison} {value}";
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
                select $"{orderBy.field} {orderBy.order}";
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