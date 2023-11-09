using MusicOrganisationTests.Lib.Enums;

namespace MusicOrganisationTests.Lib.Models
{
    public class Repertoire
    {
        private int _repertoireId;
        private DateTime _dateStarted;
        private string _syllabus = string.Empty;
        private RepertoireStatus _status;
        private string _title = string.Empty;
        private string _subtitle = string.Empty;
        private string _genre = string.Empty;
        private string _composerName = string.Empty;

        public int RepertoireId
        {
            get => _repertoireId;
            set => _repertoireId = value;
        }

        public DateTime DateStarted
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

        public string ComposerName
        {
            get => _composerName;
            set => _composerName = value;
        }
    }
}