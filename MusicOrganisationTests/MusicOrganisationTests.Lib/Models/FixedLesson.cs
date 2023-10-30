using MusicOrganisationTests.Lib.Databases;
using SQLite;

namespace MusicOrganisationTests.Lib.Models
{
    [Table(_TABLE_NAME)]
    public class FixedLesson : ISqlStorable, IEquatable<FixedLesson>
    {
        private const string _TABLE_NAME = "FixedLessons";



        public static string TableName => _TABLE_NAME;

        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public static IEnumerable<string> GetColumnNames()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetSqlValues()
        {
            throw new NotImplementedException();
        }

        public bool Equals(FixedLesson? other)
        {
            throw new NotImplementedException();
        }
    }
}
