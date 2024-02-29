using MusicOrganisationApp.Lib.Databases;
namespace MusicOrganisationApp.Lib.Tables
{
    public interface IContactablePerson : IIdentifiable
    {
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }
    }
}
