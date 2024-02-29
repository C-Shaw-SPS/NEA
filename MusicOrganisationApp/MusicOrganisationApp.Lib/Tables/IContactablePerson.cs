namespace MusicOrganisationApp.Lib.Tables
{
    public interface IContactablePerson : IPerson
    {
        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }
    }
}
