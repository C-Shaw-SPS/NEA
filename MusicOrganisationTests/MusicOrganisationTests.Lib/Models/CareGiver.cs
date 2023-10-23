using MusicOrganisationTests.Lib.Databases;
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
            throw new NotImplementedException();
        }

        public bool Equals(CareGiver? other)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetSqlValues()
        {
            throw new NotImplementedException();
        }
    }
}
