﻿using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Models
{
    public class PupilCaregiver : IEquatable<PupilCaregiver>, IContactablePerson
    {
        private int _id;
        private string _description = string.Empty;
        private int _pupilId;
        private int _caregiverId;
        private string _name = string.Empty;
        private string _emailAddress = string.Empty;
        private string _phoneNumber = string.Empty;

        public int Id
        {
            get => _id;
            set => _id = value;
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

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string EmailAddress
        {
            get => _emailAddress;
            set => _emailAddress = value;
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public int CaregiverId
        {
            get => _caregiverId;
            set => _caregiverId = value;
        }

        public bool Equals(PupilCaregiver? other)
        {
            return other is not null
                && _id == other._id
                && _description == other._description
                && _pupilId == other._pupilId
                && _name == other._name
                && _emailAddress == other._emailAddress
                && _phoneNumber == other._phoneNumber
                && _caregiverId == other._caregiverId;
        }
    }
}