using System.Text;

namespace MusicOrganisationApp.Lib.Databases
{
    public class DeleteStatement<T> : ISqlExecutable<T> where T : class, ITable, new()
    {
        private const string _WHERE = "WHERE";
        private const string _AND = "AND";
        private const string _OR = "OR";

        private readonly List<(string condition, string column, string value)> _conditions;

        public DeleteStatement()
        {
            _conditions = [];
        }

        public string TableName => T.TableName;

        public void AddWhereEqual(string column, object? value)
        {
            _conditions.Add((_WHERE, column, value.FormatSqlValue()));
        }

        public void AddAndEqual(string column, object? value)
        {
            _conditions.Add((_AND, column, value.FormatSqlValue()));
        }

        public void AddOrEqual(string column, object? value)
        {
            _conditions.Add((_OR, column, value.FormatSqlValue()));
        }

        public string GetSql()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"DELETE FROM {T.TableName}");

            IEnumerable<string> conditions = GetConditions();

            foreach (string condition in conditions)
            {
                stringBuilder.AppendLine(condition);
            }

            string sql = stringBuilder.ToString();
            return sql;
        }

        private IEnumerable<string> GetConditions()
        {
            IEnumerable<string> conditions =
                from condition in _conditions
                select $"{condition.condition} {condition.column} = {condition.value}";
            return conditions;
        }
    }
}
