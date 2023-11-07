﻿using MusicOrganisationTests.Lib.Databases;
using SQLite;

namespace MusicOrganisationTests.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class FixedLesson : ITable, IEquatable<FixedLesson>
    {
        private const string _TABLE_NAME = "FixedLessons";

        private int _id;
        private int _pupilId;
        private int _lessonTimeId;

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
        public int LessonTimeId
        {
            get => _lessonTimeId;
            set => _lessonTimeId = value;
        }

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(PupilId),
                nameof(LessonTimeId)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatValues(
                _id,
                _pupilId,
                _lessonTimeId);
        }

        public bool Equals(FixedLesson? other)
        {
            return other != null
                && _pupilId == other._pupilId
                && _lessonTimeId == other._lessonTimeId;
        }
    }
}