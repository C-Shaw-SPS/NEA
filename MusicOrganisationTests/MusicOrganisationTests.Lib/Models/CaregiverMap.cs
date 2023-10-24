﻿using MusicOrganisationTests.Lib.Databases;

namespace MusicOrganisationTests.Lib.Models
{
    public class CaregiverMap : ISqlStorable, IEquatable<CaregiverMap>
    {
        private const string _TABLE_NAME = "CaregiverMap";

        private int _id;
        private int _pupilId;
        private int _caregiverId;
        private string _description = string.Empty;

        public static string TableName => _TABLE_NAME;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int PupilId
        {
            get => _pupilId;
            set => _pupilId = value;
        }

        public int CaregiverId
        {
            get => _caregiverId;
            set => _caregiverId = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }


        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(PupilId),
                nameof(CaregiverId),
                nameof(Description)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatAsSqlValues(
                _id,
                _pupilId,
                _caregiverId,
                _description);
        }
        public bool Equals(CaregiverMap? other)
        {
            return other != null
                && _id == other._id
                && _pupilId == other._pupilId
                && _caregiverId == other._caregiverId
                && _description == other._description;
        }
    }
}
