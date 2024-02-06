using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Json
{
    internal static class ExpectedJson
    {
        public static readonly ComposerData Composer = new()
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

        public static readonly List<ComposerData> ResponseComposers = new()
        {
            new ComposerData
            {
                Id = 36,
                Name = "Ralph Vaughan Williams",
                BirthYear = 1872,
                DeathYear = 1958,
                Era = "Late Romantic"
            },
            new ComposerData
            {
                Id = 87,
                Name = "Johann Sebastian Bach",
                BirthYear =1685,
                DeathYear = 1750,
                Era = "Baroque"
            },
            new ComposerData
            {
                Id = 176,
                Name = "Steve Reich",
                BirthYear = 1936,
                DeathYear = null,
                Era = "Post-War"
            },
            new ComposerData
            {
                Id = 196,
                Name = "Wolfgang Amadeus Mozart",
                BirthYear = 1756,
                DeathYear = 1791,
                Era = "Classical"
            },
            new ComposerData()
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