using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.ViewModels
{
    public static class ExpectedViewModels
    {
        public static readonly List<Composer> Composers = new()
        {
            new Composer
            {
                Id = 36,
                Name = "Ralph Vaughan Williams",
                BirthYear = 1872,
                DeathYear = 1958,
                Era = "Late Romantic"
            },
            new Composer
            {
                Id = 87,
                Name = "Johann Sebastian Bach",
                BirthYear =1685,
                DeathYear = 1750,
                Era = "Baroque"
            },
            new Composer
            {
                Id = 176,
                Name = "Steve Reich",
                BirthYear = 1936,
                DeathYear = null,
                Era = "Post-War"
            },
            new Composer
            {
                Id = 196,
                Name = "Wolfgang Amadeus Mozart",
                BirthYear = 1756,
                DeathYear = 1791,
                Era = "Classical"
            },
            new Composer()
            {
                Id = 91,
                Name = "Kenji Bunch",
                BirthYear = 1973,
                DeathYear = null,
                Era = "21st Century",
            }
        };
    }
}