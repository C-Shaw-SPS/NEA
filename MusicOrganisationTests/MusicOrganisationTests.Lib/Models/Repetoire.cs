using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Lib.Models
{
    [Table(_TABLE_NAME)]
    public class Repetoire : ISqlStorable, IEquatable<Repetoire>
    {
        private const string _TABLE_NAME = "Repetoire";

        private int _id;
        private int _pupilId;
        private int _workId;
        private DateOnly _dateStarted;
        private string _syllabus = string.Empty;
        private RepetoireStatus _status;

        public static string TableName => _TABLE_NAME;

        [AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public int PupilId
        {
            get
            {
                return _pupilId;
            }
            set
            {
                _pupilId = value;
            }
        }

        public int WorkId
        {
            get
            {
                return _workId;
            }
            set
            {
                _workId = value;
            }
        }

        public DateOnly DateStarted
        {
            get
            {
                return _dateStarted;
            }
            set
            {
                _dateStarted = value;
            }
        }

        public string Syllabus
        {
            get
            {
                return _syllabus;
            }
            set
            {
                _syllabus = value;
            }
        }

        public RepetoireStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(PupilId),
                nameof(WorkId),
                nameof(DateStarted),
                nameof(Syllabus),
                nameof(Status)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatAsSqlValues(
                _id,
                _pupilId,
                _workId,
                _dateStarted,
                _syllabus,
                _status);
        }

        public bool Equals(Repetoire? other)
        {
            throw new NotImplementedException();
        }
    }
}
