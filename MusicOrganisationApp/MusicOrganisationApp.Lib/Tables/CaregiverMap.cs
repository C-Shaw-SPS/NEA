﻿using MusicOrganisationApp.Lib.Databases;
using SQLite;

namespace MusicOrganisationApp.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class CaregiverMap : ITable, IEquatable<CaregiverMap>, IPupilIdentifiable
    {
        private const string _TABLE_NAME = nameof(CaregiverMap);

        private int _id;
        private int _pupilId;
        private int _caregiverId;
        private string _description = string.Empty;

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
        public int CaregiverId
        {
            get => _caregiverId;
            set => _caregiverId = value;
        }

        [NotNull]
        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public static IEnumerable<string> GetFieldNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(PupilId),
                nameof(CaregiverId),
                nameof(Description)
            };
        }

        public IDictionary<string, string> GetSqlValues()
        {
            IDictionary<string, string> sqlValues = SqlFormatting.FormatValues(
                (nameof(Id), _id),
                (nameof(PupilId), _pupilId),
                (nameof(CaregiverId), _caregiverId),
                (nameof(Description), _description)
                );
            return sqlValues;
        }
        public bool Equals(CaregiverMap? other)
        {
            return other is not null
                && _id == other._id
                && _pupilId == other._pupilId
                && _caregiverId == other._caregiverId
                && _description == other._description;
        }
    }
}