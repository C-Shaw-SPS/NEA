using MusicOrganisationApp.Lib.Enums;

namespace MusicOrganisationApp.Lib.Models
{
    public class Repertoire : IEquatable<Repertoire>
    {
        private int _repertoireId;
        private DateTime? _dateStarted;
        private string _syllabus = string.Empty;
        private RepertoireStatus _status;
        private int _workId;
        private string _title = string.Empty;
        private string _subtitle = string.Empty;
        private int _composerId;
        private string _genre = string.Empty;
        private string _composerName = string.Empty;

        public int RepertoireId
        {
            get => _repertoireId;
            set => _repertoireId = value;
        }

        public DateTime? DateStarted
        {
            get => _dateStarted;
            set => _dateStarted = value;
        }

        public string Syllabus
        {
            get => _syllabus;
            set => _syllabus = value;
        }

        public RepertoireStatus Status
        {
            get => _status;
            set => _status = value;
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

        public bool Equals(Repertoire? other)
        {
            return other != null
                && _repertoireId == other._repertoireId
                && _dateStarted == other._dateStarted
                && _syllabus == other._syllabus
                && _status == other._status
                && _workId == other._workId
                && _title == other._title
                && _subtitle == other._subtitle
                && _composerId == other._composerId
                && _genre == other._genre
                && _composerName == other._composerName;
        }
    }
}