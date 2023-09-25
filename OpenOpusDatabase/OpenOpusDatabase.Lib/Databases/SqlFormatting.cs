﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.Databases
{
    internal static class SqlFormatting
    {
        public const string DB_FILE_EXTENSION = ".db";
        public const string COMMA_SEPARATOR = ",";
        public const string OPEN_BRACKET = "(";
        public const string CLOSE_BRACKET = ")";
        public const string NULL = "NULL";
        public const char DOUBLE_QUOTE = '\"';
        public const char SINGLE_QUOTE = '\'';

        public static string FormatAsDatabasePath(this string s)
        {
            if (!s.EndsWith(DB_FILE_EXTENSION))
            {
                return s + DB_FILE_EXTENSION;
            }
            else
            {
                return s;
            }
        }

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
