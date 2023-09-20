using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.Databases
{
    internal static class TableName
    {
        public static string GetTableName<T>()
        {
            string tableName = typeof(T).Name;
            object[] customAttributes = typeof(T).GetCustomAttributes(typeof(TableAttribute), false);
            if (customAttributes.Length > 0)
            {
                TableAttribute? tableAttribute = customAttributes[0] as TableAttribute;
                if (tableAttribute != null)
                {
                    tableName = tableAttribute.Name;
                }
            }
            return tableName;
        }
    }
}
