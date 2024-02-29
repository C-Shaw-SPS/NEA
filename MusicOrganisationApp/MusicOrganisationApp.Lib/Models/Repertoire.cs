using MusicOrganisationApp.Lib.Databases;

namespace MusicOrganisationApp.Lib.Models
{
    public class Repertoire : IEquatable<Repertoire>, IIdentifiable
    {
        private int _id;
        private DateTime? _dateStarted;
        private string _syllabus = string.Empty;
        private bool _isFinishedLearning;
        private int _pupilId;
        private int _workId;
        private string _title = string.Empty;
        private string _subtitle = string.Empty;
        private int _composerId;
        private string _genre = string.Empty;
        private string _composerName = string.Empty;
        private string _notes = string.Empty;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public DateTime? DateStarted
        {
            get => _dateStarted;
            set
            {
                if (value is DateTime dateTime)
                {
                    _dateStarted = dateTime.Date;
                }
                else
                {
                    _dateStarted = value;
                }
            }
        }

        public string Syllabus
        {
            get => _syllabus;
            set => _syllabus = value;
        }

        public bool IsFinishedLearning
        {
            get => _isFinishedLearning;
            set => _isFinishedLearning = value;
        }

        public int PupilId
        {
            get => _pupilId;
            set => _pupilId = value;
        }

        public int WorkId
        {
            get => _workId;
            set => _workId = value;
        }

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public string Subtitle
        {
            get => _subtitle;
            set => _subtitle = value;
        }

        public string Genre
        {
            get => _genre;
            set => _genre = value;
        }

        public int ComposerId
        {
            get => _composerId;
            set => _composerId = value;
        }

        public string ComposerName
        {
            get => _composerName;
            set => _composerName = value;
        }

        public string Notes
        {
            get => _notes;
            set => _notes = value;
        }

        public bool Equals(Repertoire? other)
        {
            return other is not null
                && _id == other._id
                && _dateStarted == other._dateStarted
                && _syllabus == other._syllabus
                && _isFinishedLearning == other._isFinishedLearning
                && _pupilId == other._pupilId
                && _workId == other._workId
                && _title == other._title
                && _subtitle == other._subtitle
                && _composerId == other._composerId
                && _genre == other._genre
                && _composerName == other._composerName
                && _notes == other._notes;
        }
    }
}