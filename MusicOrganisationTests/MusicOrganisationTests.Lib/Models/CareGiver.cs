﻿using MusicOrganisationTests.Lib.Databases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Lib.Models
{
    [Table(_TABLE_NAME)]
    public class CareGiver : ISqlStorable, IEquatable<CareGiver>
    {
        private const string _TABLE_NAME = "CareGivers";

        private int _id;
        private string _name = string.Empty;
        private string? _email;
        private string? _phoneNumber;

        public static string TableName => _TABLE_NAME;

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

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>()
            {
                nameof(Id),
                nameof(Name),
                nameof(Email),
                nameof(PhoneNumber)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatAsSqlValues(
                _id,
                _name,
                _email,
                _phoneNumber);
        }

        public bool Equals(CareGiver? other)
        {
            return other != null
                && _id == other._id
                && _name == other._name
                && _email == other._email
                && _phoneNumber == other._phoneNumber;
        }
    }
}