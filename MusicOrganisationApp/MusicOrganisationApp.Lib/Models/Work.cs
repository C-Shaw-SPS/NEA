namespace MusicOrganisationApp.Lib.Models
{
    public class Work : IEquatable<Work>, IModel
    {
        private int _id;
        private int _composerId;
        private string _title = string.Empty;
        private string _subtitle = string.Empty;
        private string _genre = string.Empty;
        private string _composerName = string.Empty;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int ComposerId
        {
            get => _composerId;
            set => _composerId = value;
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

        public bool Equals(Work? other)
        {
            return other is not null &&
                   _id == other._id &&
                   _composerId == other._composerId &&
                   _title == other._title &&
                   _subtitle == other._subtitle &&
                   _genre == other._genre &&
                   _composerName == other._composerName;
        }
    }
}