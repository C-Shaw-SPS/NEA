﻿using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using SQLite;

namespace MusicOrganisationApp.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class LessonSlotData : ITable, IEquatable<LessonSlotData>, ILesson<LessonSlotData>
    {
        private const string _TABLE_NAME = nameof(LessonSlotData);

        private int _id;
        private DayOfWeek _dayOfWeek;
        private TimeSpan _startTime;
        private TimeSpan _endTime;

        public static string TableName => _TABLE_NAME;

        [PrimaryKey]
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        [NotNull]
        public DayOfWeek DayOfWeek
        {
            get => _dayOfWeek;
            set => _dayOfWeek = value;
        }

        [NotNull]
        public TimeSpan StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        [NotNull]
        public TimeSpan EndTime
        {
            get => _endTime;
            set => _endTime = value;
        }

        [Ignore]
        public TimeSpan Duration => _endTime - _startTime;

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(DayOfWeek),
                nameof(StartTime),
                nameof(EndTime)
            };
        }

        public IDictionary<string, string> GetSqlValues()
        {
            IDictionary<string, string> sqlValues = SqlFormatting.FormatValues(
                (nameof(Id), _id),
                (nameof(DayOfWeek), _dayOfWeek),
                (nameof(StartTime), _startTime),
                (nameof(EndTime), _endTime)
                );
            return sqlValues;
        }

        public bool Equals(LessonSlotData? other)
        {
            return other != null
                && _id == other._id
                && _dayOfWeek == other._dayOfWeek
                && _startTime == other._startTime
                && _endTime == other._endTime;
        }
    }
}