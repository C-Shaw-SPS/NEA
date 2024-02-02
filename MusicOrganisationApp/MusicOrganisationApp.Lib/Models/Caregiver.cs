namespace MusicOrganisationApp.Lib.Models
{
    public class Caregiver : IEquatable<Caregiver>, IIdentifiable
    {
        private int _id;
        private string _name = string.Empty;
        private string _email = string.Empty;
        private string _phoneNumber = string.Empty; 
        private int _mapId;
        private string _description = string.Empty;
        private int _pupilId;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public int MapId
        {
            get => _mapId;
            set => _mapId = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public int PupilId
        {
            get => _pupilId;
            set => _pupilId = value;
        }

        public bool Equals(Caregiver? other)
        {
            return other != null
                && _id == other._id
                && _name == other._name
                && _email == other._email
                && _phoneNumber == other._phoneNumber
                && _mapId == other._mapId
                && _description == other._description
                && _pupilId == other._pupilId;
        }
    }
}