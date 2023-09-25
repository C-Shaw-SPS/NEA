using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.Databases
{
    internal static class SqlFormatting
    {
        private const string COMMA_SEPARATOR = ",";
        private const string OPEN_BRACKET = "(";
        private const string CLOSE_BRACKET = ")";
        private const string NULL = "NULL";
        private const char DOUBLE_QUOTE = '\"';
        private const char SINGLE_QUOTE = '\'';

        public static string CommaJoin(this List<string> list)
        {
            return OPEN_BRACKET + String.Join(COMMA_SEPARATOR, list) + CLOSE_BRACKET;
        }

        public static List<string> FormatAsSqlValues(this List<object?> values)
        {
            List<string> result = new();
            foreach (object? value in values)
            {
                if (value == null)
                {
                    result.Add(NULL);
                }
                else if (value is string s)
                {
                    result.Add(s.FormatSqlString());
                }
                else if (value is DateTime dateTime)
                {
                    result.Add(dateTime.FormatSqlDateTime());
                }
                else
                {
                    result.Add(value.ToString());
                }
            }
            return result;
        }

        private static string FormatSqlString(this string s)
        {
            return $"\"{s.ReplaceQuotes()}\"";
        }

        private static string ReplaceQuotes(this string s)
        {
            StringBuilder result = new();
            foreach (char c in s)
            {
                if (c == DOUBLE_QUOTE)
                {
                    result.Append(SINGLE_QUOTE);
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        private static string FormatSqlDateTime(this DateTime dateTime)
        {
            return dateTime.Ticks.ToString();
        }
    }
}
