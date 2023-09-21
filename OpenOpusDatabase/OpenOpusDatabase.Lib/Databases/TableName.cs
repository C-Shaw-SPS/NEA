using SQLite;

namespace OpenOpusDatabase.Lib.Databases
{
    internal static class TableName
    {
        /// <summary>
        /// Returns the table name of the specified type, returning the name of the type if the type does not have the TableAttribute attribute
        /// </summary>
        public static string Get<T>()
        {
            string tableName = typeof(T).Name;
            object[] customAttributes = typeof(T).GetCustomAttributes(typeof(TableAttribute), false);
            if (customAttributes.Length > 0)
            {
                if (customAttributes[0] is TableAttribute tableAttribute)
                {
                    tableName = tableAttribute.Name;
                }
            }
            return tableName;
        }
    }
}