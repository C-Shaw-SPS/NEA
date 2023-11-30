namespace MusicOrganisationTests.Lib.Models
{
    public class Caregiver : IEquatable<Caregiver>
    {
        private int _mapId;
        private int _caregiverId;
        private string _name = string.Empty;
        private string _description = string.Empty;
        private string? _email;
        private string? _phoneNumber;

        public int MapId
        {
            get => _mapId;
            set => _mapId = value;
        }

        public int CaregiverId
        {
            get => _caregiverId;
            set => _caregiverId = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public string? Email
        {
            get => _email;
            set => _email = value;
        }

        public string? PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public bool Equals(Caregiver? other)
        {
            return other != null
                && _caregiverId == other._caregiverId
                && _name == other._name
                && _description == other._description
                && _email == other._email
                && _phoneNumber == other._phoneNumber;
        }
    }
}