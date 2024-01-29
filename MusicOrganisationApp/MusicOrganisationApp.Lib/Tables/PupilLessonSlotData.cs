using MusicOrganisationApp.Lib.Databases;
using SQLite;

namespace MusicOrganisationApp.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class PupilLessonSlotData : ITable, IEquatable<PupilLessonSlotData>, IPupilIdentifiable
    {
        private const string _TABLE_NAME = nameof(PupilLessonSlotData);

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

        [NotNull]
        public int LessonSlotId
        {
            get => _lessonSlotId;
            set => _lessonSlotId = value;
        }

        public static IEnumerable<string> GetColumnNames()
        {
            List<string> columnNames = new()
            {
                nameof(Id),
                nameof(PupilId),
                nameof(LessonSlotId)
            };
            return columnNames;
        }

        public IDictionary<string, string> GetSqlValues()
        {
            IDictionary<string, string> sqlValues = SqlFormatting.FormatValues(
                (nameof(Id), _id),
                (nameof(PupilId), _pupilId),
                (nameof(LessonSlotId), _lessonSlotId)
                );
            return sqlValues;
        }

        public bool Equals(PupilLessonSlotData? other)
        {
            return other is not null
                && _id == other._id
                && _pupilId == other._pupilId
                && _lessonSlotId == other._lessonSlotId;
        }
    }
}