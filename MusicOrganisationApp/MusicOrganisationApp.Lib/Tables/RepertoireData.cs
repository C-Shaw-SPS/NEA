﻿using MusicOrganisationApp.Lib.Databases;
using SQLite;

namespace MusicOrganisationApp.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class RepertoireData : ITable, IEquatable<RepertoireData>, IPupilIdentifiable
    {
        private const string _TABLE_NAME = nameof(RepertoireData);

        private int _id;
        private int _pupilId;
        private int _workId;
        private DateTime? _dateStarted;
        private string _syllabus = string.Empty;
        private bool _isFinishedLearning;
        private string _notes = string.Empty;

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
        public int WorkId
        {
            get => _workId;
            set => _workId = value;
        }

        public DateTime? DateStarted
        {
            get => _dateStarted;
            set => _dateStarted = value;
        }

        [NotNull]
        public string Syllabus
        {
            get => _syllabus;
            set => _syllabus = value;
        }

        [NotNull]
        public bool IsFinishedLearning
        {
            get => _isFinishedLearning;
            set => _isFinishedLearning = value;
        }

        [NotNull]
        public string Notes
        {
            get => _notes;
            set => _notes = value;
        }

        public static IEnumerable<string> GetFieldNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(PupilId),
                nameof(WorkId),
                nameof(DateStarted),
                nameof(Syllabus),
                nameof(IsFinishedLearning),
                nameof(Notes)
            };
        }

        public IDictionary<string, string> GetSqlValues()
        {
            IDictionary<string, string> sqlValues = SqlFormatting.FormatValues(
                (nameof(Id), _id),
                (nameof(PupilId), _pupilId),
                (nameof(WorkId), _workId),
                (nameof(DateStarted), _dateStarted),
                (nameof(Syllabus), _syllabus),
                (nameof(IsFinishedLearning), _isFinishedLearning),
                (nameof(Notes), _notes)
                );
            return sqlValues;
        }

        public bool Equals(RepertoireData? other)
        {
            return other is not null
                && _id == other._id
                && _pupilId == other._pupilId
                && _workId == other._workId
                && _dateStarted == other._dateStarted
                && _syllabus == other._syllabus
                && _isFinishedLearning == other._isFinishedLearning
                && _notes == other._notes;
        }
    }
}