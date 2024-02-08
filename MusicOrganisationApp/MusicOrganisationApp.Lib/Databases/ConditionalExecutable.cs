namespace MusicOrganisationApp.Lib.Databases
{
    public abstract class ConditionalExecutable<T> : ISqlExecutable<T> where T : class, ITable, new()
    {
        protected readonly List<(string keyword, string table, string column, string value, string comparison)> _conditions = [];

        public void AddWhereEqual<TTable>(string column, object? value) where TTable : ITable
        {
            AddCondition<TTable>(ISqlExecutable.WHERE, column, value, ISqlExecutable.EQUALS);
        }

        public void AddWhereLike<TTable>(string column, string value) where TTable : ITable
        {
            AddCondition<TTable>(ISqlExecutable.WHERE, column, $"%{value}%", ISqlExecutable.LIKE);
        }

        public void AddAndEqual<TTable>(string column, object? value) where TTable : ITable
        {
            string condition = value is null ? ISqlExecutable.IS : ISqlExecutable.EQUALS;
            AddCondition<TTable>(ISqlExecutable.AND, column, value, condition);
        }

        public void AddOrEqual<TTable>(string column, object? value) where TTable : ITable
        {
            string condition = value is null ? ISqlExecutable.IS : ISqlExecutable.EQUALS;
            AddCondition<TTable>(ISqlExecutable.OR, column, value, condition);
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
            string condition = value is null ? ISqlExecutable.IS_NOT : ISqlExecutable.NOT_EQUAL;
            AddCondition<TTable>(ISqlExecutable.AND, column, value, condition);
        }

        private void AddCondition<TTable>(string keyword, string column, object? value, string condition) where TTable : ITable
        {
            _conditions.Add((keyword, TTable.TableName, column, SqlFormatting.FormatSqlValue(value), condition));
        }

        public abstract string GetSql();
    }
}