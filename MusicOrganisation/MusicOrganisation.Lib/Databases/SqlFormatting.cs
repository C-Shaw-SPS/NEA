using MusicOrganisationTests.Lib.Enums;
using System.Text;

namespace MusicOrganisation.Lib.Databases
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

        public static string CommaJoin(this IEnumerable<string> list)
        {
            return string.Join(COMMA_SEPARATOR, list);
        }

        public static string AddBrackets(this string s)
        {
            return OPEN_BRACKET + s + CLOSE_BRACKET;
        }

        public static IEnumerable<string> FormatValues(params object?[] values)
        {
            List<string> result = new();
            foreach (object? value in values)
            {
                result.Add(FormatValue(value));
            }
            return result;
        }

        public static string FormatValue(object? value)
        {
            if (value == null)
            {
                return NULL;
            }
            else if (value is string s)
            {
                return s.FormatSqlString();
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
                return day.FormatSqlDay();
            }
            else if (value is RepertoireStatus repertoireStatus)
            {
                return repertoireStatus.FormatSqlRepertoireStatus();
            }
            else
            {
                return value.ToStringOrNull();
            }
        }

        public static string FormatLikeString(string value)
        {
            return $"%{value}%".FormatSqlString();
        }

        private static string FormatSqlString(this string s)
        {
            return DOUBLE_QUOTE + s.ReplaceQuotes() + DOUBLE_QUOTE;
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

        private static string FormatSqlTimeSpan(this TimeSpan timeSpan)
        {
            return timeSpan.Ticks.ToString();
        }

        private static string FormatSqlDay(this DayOfWeek day)
        {
            return ((int)day).ToString();
        }

        private static string FormatSqlRepertoireStatus(this RepertoireStatus repertoireStatus)
        {
            return ((int)repertoireStatus).ToString();
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