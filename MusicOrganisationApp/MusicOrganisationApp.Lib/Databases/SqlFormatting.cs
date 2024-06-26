﻿using System.Text;

namespace MusicOrganisationApp.Lib.Databases
{
    public static class SqlFormatting
    {
        public const string DB_FILE_EXTENSION = ".db";
        public const string COMMA_SEPARATOR = ",";
        public const string OPEN_BRACKET = "(";
        public const string CLOSE_BRACKET = ")";
        public const string NULL = "NULL";
        public const string ENCODED_SINGLE_QUOTE = "''";
        public const char SINGLE_QUOTE = '\'';

        public static string FormatAsDatabasePath(this string s)
        {
            if (!s.EndsWith(DB_FILE_EXTENSION))
            {
                s += DB_FILE_EXTENSION;
            }
            return s;
        }

        public static string CommaJoin(this IEnumerable<string> list)
        {
            return string.Join(COMMA_SEPARATOR, list);
        }

        public static string AddBrackets(this string s)
        {
            return OPEN_BRACKET + s + CLOSE_BRACKET;
        }

        public static IDictionary<string, string> FormatValues(params (string name, object? value)[] values)
        {
            Dictionary<string, string> result = [];
            foreach ((string name, object? value) in values)
            {
                result.Add(name, FormatSqlValue(value));
            }
            return result;
        }

        public static string FormatSqlValue(this object? value)
        {
            if (value == null)
            {
                return NULL;
            }
            else if (value is string s)
            {
                return s.FormatSqlString();
            }
            else if (value is bool b)
            {
                return b.FormatSqlBool();
            }
            else if (value is DateTime dateTime)
            {
                return dateTime.FormatSqlDateTime();
            }
            else if (value is TimeSpan timeSpan)
            {
                return timeSpan.FormatSqlTimeSpan();
            }
            else if (value is DayOfWeek day)
            {
                return day.FormatSqlDayOfWeek();
            }
            else if (value is int n)
            {
                return n.ToString();
            }
            else
            {
                throw new Exception($"Invalid type: {value.GetType().Name}");
            }
        }

        public static string FormatLikeString(string value)
        {
            return $"%{value}%".FormatSqlString();
        }

        private static string FormatSqlString(this string s)
        {
            return string.Join(string.Empty, SINGLE_QUOTE, s.ReplaceQuotes(), SINGLE_QUOTE);
        }

        private static string ReplaceQuotes(this string s)
        {
            StringBuilder result = new();
            foreach (char c in s)
            {
                if (c == SINGLE_QUOTE)
                {
                    result.Append(ENCODED_SINGLE_QUOTE);
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        private static string FormatSqlBool(this bool b)
        {
            return b.ToString();
        }

        private static string FormatSqlDateTime(this DateTime dateTime)
        {
            return dateTime.Ticks.ToString();
        }

        private static string FormatSqlTimeSpan(this TimeSpan timeSpan)
        {
            return timeSpan.Ticks.ToString();
        }

        private static string FormatSqlDayOfWeek(this DayOfWeek day)
        {
            return ((int)day).ToString();
        }

        private static string ToStringOrNull(this object value)
        {
            string? possiblyNullString = value.ToString();
            if (possiblyNullString is string notNullString)
            {
                return notNullString;
            }
            else
            {
                return NULL;
            }
        }
    }
}