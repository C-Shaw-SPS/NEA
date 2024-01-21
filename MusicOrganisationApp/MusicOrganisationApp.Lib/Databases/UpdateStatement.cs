using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.Databases
{
    public abstract class UpdateStatement
    {
        private readonly string _tableName;
        private readonly int _id;
        private readonly IDictionary<string, string> _sqlValues;
        private readonly IList<string> _columnsToUpdate;

        public UpdateStatement(string tableName, int id)
        {
            _tableName = tableName;
            _id = id;
            _sqlValues = new Dictionary<string, string>();
            _columnsToUpdate = [];
        }

        public string TableName => _tableName;

        public void AddColumnToUpdate(string columnName, object? value)
        {
            string sqlValue = SqlFormatting.FormatSqlValue(value);
            _sqlValues[columnName] = sqlValue;
        }

        public string GetSql()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"UPDATE {_tableName}");
            stringBuilder.AppendLine("SET");
            List<string> setValues = [];
            foreach (string column in _sqlValues.Keys)
            {
                setValues.Add($"{column} = {_sqlValues[column]}");
            }
            stringBuilder.AppendLine(string.Join(",\n", setValues));
            stringBuilder.AppendLine($"WHERE {nameof(ITable.Id)} = {_id}");
            string statement = stringBuilder.ToString();
            return statement;
        }

        public static UpdateStatement GetUpdateAllColumns<T>(T value) where T : ITable
        {
            UpdateStatement<T> updateStatement = new(value.Id);
            IDictionary<string, string> sqlValues = value.GetSqlValues();
            foreach (string column in sqlValues.Keys)
            {
                updateStatement.AddColumnToUpdate(column, sqlValues[column]);
            }
            return updateStatement;
        }
    }

    public class UpdateStatement<T> : UpdateStatement where T : ITable
    {
        public UpdateStatement(int id) : base(T.TableName, id) { }
    }
}