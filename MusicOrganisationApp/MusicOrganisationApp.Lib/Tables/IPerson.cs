using MusicOrganisationApp.Lib.Databases;

namespace MusicOrganisationApp.Lib.Tables
{
    public interface IPerson : IIdentifiable
    {
        public string Name { get; set; }
    }
}