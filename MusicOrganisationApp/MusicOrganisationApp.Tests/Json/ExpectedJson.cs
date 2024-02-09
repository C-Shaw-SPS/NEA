using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Json
{
    internal static class ExpectedJson
    {
        public static readonly Composer Composer = new()
        {
            Id = 87,
            Name = "Johann Sebastian Bach",
            BirthYear = 1685,
            DeathYear = 1750,
            Era = "Baroque"
        };

        public static readonly WorkData Work = new()
        {
            Id = 20086,
            ComposerId = 176,
            Title = "3 Movements",
            Subtitle = "",
            Genre = "Orchestral"
        };

        public static readonly List<Composer> ResponseComposers = new()
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

        public static readonly List<WorkData> ResponseWorks = new()
        {
            new WorkData
            {
                Id = 20086,
                ComposerId = 176,
                Title = "3 Movements",
                Subtitle = "",
                Genre = "Orchestral"
            },
            new WorkData
            {
                Id = 25115,
                ComposerId = 202,
                Title = "3 Mouvements perp\u00e9tuels, FP14",
                Subtitle = "",
                Genre = "Keyboard"
            }
        };

    }
}