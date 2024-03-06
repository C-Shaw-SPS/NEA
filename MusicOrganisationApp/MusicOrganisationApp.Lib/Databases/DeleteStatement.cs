using System.Text;

namespace MusicOrganisationApp.Lib.Databases
{
    public class DeleteStatement<T> : ConditionalExecutable<T> where T : class, ITable, new()
    {
        public DeleteStatement()
        {
        }

        public string TableName => T.TableName;

        public override string GetSql()
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
                select $"{condition.keyword} {condition.field} {condition.comparison} {condition.value}";
            return conditions;
        }
    }
}