using MusicOrganisationTests.Lib.Databases;
using SQLite;

namespace MusicOrganisationTests.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class FixedLessonData : ITable, IEquatable<FixedLessonData>
    {
        private const string _TABLE_NAME = nameof(FixedLessonData);

        private int _id;
        private int _pupilId;
        private int _lessonSlotId;

        public static string TableName => _TABLE_NAME;

        [PrimaryKey]
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        [NotNull]
        public int PupilId
        {
            get => _pupilId;
            set => _pupilId = value;
        }

        [NotNull, Unique]
        public int LessonSlotId
        {
            get => _lessonSlotId;
            set => _lessonSlotId = value;
        }

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(PupilId),
                nameof(LessonSlotId)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatValues(
                _id,
                _pupilId,
                _lessonSlotId);
        }

        public bool Equals(FixedLessonData? other)
        {
            return other != null
                && _pupilId == other._pupilId
                && _lessonSlotId == other._lessonSlotId;
        }
    }
}