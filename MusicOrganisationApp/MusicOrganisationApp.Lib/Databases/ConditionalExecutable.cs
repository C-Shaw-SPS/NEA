namespace MusicOrganisationApp.Lib.Databases
{
    public abstract class ConditionalExecutable<T> : ISqlExecutable<T> where T : class, ITable, new()
    {
        protected readonly List<(string keyword, string table, string column, string value, string comparison)> _conditions = [];

        public void AddWhereEqual<TTable>(string column, object? value) where TTable : ITable
        {
            string comparison = value is null ? ISqlExecutable.IS : ISqlExecutable.EQUALS;
            AddCondition<TTable>(ISqlExecutable.WHERE, column, value, comparison);
        }

        public void AddWhereLike<TTable>(string column, string value) where TTable : ITable
        {
            AddCondition<TTable>(ISqlExecutable.WHERE, column, $"%{value}%", ISqlExecutable.LIKE);
        }

        public void AddWhereGreaterOrEqual<TTable>(string column, object value) where TTable : ITable
        {
            AddCondition<TTable>(ISqlExecutable.WHERE, column, value, ISqlExecutable.GREATER_OR_EQUAL);
        }

        public void AddAndEqual<TTable>(string column, object? value) where TTable : ITable
        {
            string comparison = value is null ? ISqlExecutable.IS : ISqlExecutable.EQUALS;
            AddCondition<TTable>(ISqlExecutable.AND, column, value, comparison);
        }

        public void AddOrEqual<TTable>(string column, object? value) where TTable : ITable
        {
            string comparison = value is null ? ISqlExecutable.IS : ISqlExecutable.EQUALS;
            AddCondition<TTable>(ISqlExecutable.OR, column, value, comparison);
        }

        public void AddAndGreaterThan<TTable>(string column, object value) where TTable : ITable
        {
            AddCondition<TTable>(ISqlExecutable.AND, column, value, ISqlExecutable.GREATER);
        }

        public void AddAndGreaterOrEqual<TTable>(string column, object value) where TTable : ITable
        {
            AddCondition<TTable>(ISqlExecutable.AND, column, value, ISqlExecutable.GREATER_OR_EQUAL);
        }

        public void AddAndLessThan<TTable>(string column, object value) where TTable : ITable
        {
            AddCondition<TTable>(ISqlExecutable.AND, column, value, ISqlExecutable.LESS);
        }

        public void AddAndLessOrEqual<TTable>(string column, object value) where TTable : ITable
        {
            AddCondition<TTable>(ISqlExecutable.AND, column, value, ISqlExecutable.LESS_OR_EQUAL);
        }

        public void AddAndNotEqual<TTable>(string column, object? value) where TTable : ITable
        {
            string comparison = value is null ? ISqlExecutable.IS_NOT : ISqlExecutable.NOT_EQUAL;
            AddCondition<TTable>(ISqlExecutable.AND, column, value, comparison);
        }

        private void AddCondition<TTable>(string keyword, string column, object? value, string comparison) where TTable : ITable
        {
            _conditions.Add((keyword, TTable.TableName, column, SqlFormatting.FormatSqlValue(value), comparison));
        }

        public abstract string GetSql();
    }
}