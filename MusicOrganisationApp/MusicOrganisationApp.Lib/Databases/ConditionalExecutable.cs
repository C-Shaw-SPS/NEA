namespace MusicOrganisationApp.Lib.Databases
{
    public abstract class ConditionalExecutable<T> : ISqlExecutable<T> where T : class, ITable, new()
    {
        protected readonly List<(string keyword, string table, string field, string value, string comparison)> _conditions = [];

        public void AddWhereEqual<TTable>(string field, object? value) where TTable : ITable
        {
            string comparison = value is null ? ISqlExecutable.IS : ISqlExecutable.EQUALS;
            AddCondition<TTable>(ISqlExecutable.WHERE, field, value, comparison);
        }

        public void AddWhereLike<TTable>(string field, string value) where TTable : ITable
        {
            AddCondition<TTable>(ISqlExecutable.WHERE, field, $"%{value}%", ISqlExecutable.LIKE);
        }

        public void AddWhereGreaterOrEqual<TTable>(string field, object value) where TTable : ITable
        {
            AddCondition<TTable>(ISqlExecutable.WHERE, field, value, ISqlExecutable.GREATER_OR_EQUAL);
        }

        public void AddAndEqual<TTable>(string field, object? value) where TTable : ITable
        {
            string comparison = value is null ? ISqlExecutable.IS : ISqlExecutable.EQUALS;
            AddCondition<TTable>(ISqlExecutable.AND, field, value, comparison);
        }

        public void AddOrEqual<TTable>(string field, object? value) where TTable : ITable
        {
            string comparison = value is null ? ISqlExecutable.IS : ISqlExecutable.EQUALS;
            AddCondition<TTable>(ISqlExecutable.OR, field, value, comparison);
        }

        public void AddAndGreaterThan<TTable>(string field, object value) where TTable : ITable
        {
            AddCondition<TTable>(ISqlExecutable.AND, field, value, ISqlExecutable.GREATER);
        }

        public void AddAndGreaterOrEqual<TTable>(string field, object value) where TTable : ITable
        {
            AddCondition<TTable>(ISqlExecutable.AND, field, value, ISqlExecutable.GREATER_OR_EQUAL);
        }

        public void AddAndLessThan<TTable>(string field, object value) where TTable : ITable
        {
            AddCondition<TTable>(ISqlExecutable.AND, field, value, ISqlExecutable.LESS);
        }

        public void AddAndLessOrEqual<TTable>(string field, object value) where TTable : ITable
        {
            AddCondition<TTable>(ISqlExecutable.AND, field, value, ISqlExecutable.LESS_OR_EQUAL);
        }

        public void AddAndNotEqual<TTable>(string field, object? value) where TTable : ITable
        {
            string comparison = value is null ? ISqlExecutable.IS_NOT : ISqlExecutable.NOT_EQUAL;
            AddCondition<TTable>(ISqlExecutable.AND, field, value, comparison);
        }

        private void AddCondition<TTable>(string keyword, string field, object? value, string comparison) where TTable : ITable
        {
            _conditions.Add((keyword, TTable.TableName, field, SqlFormatting.FormatSqlValue(value), comparison));
        }

        public abstract string GetSql();
    }
}