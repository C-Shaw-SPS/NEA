using OpenOpusDatabase.Lib.Models;

namespace OpenOpusDatabase.Tests
{
    internal static class Expected
    {
        public static readonly List<Composer> Composers = new()
        {
            new Composer
            {
                Id = 36,
                Name = "Vaughan Williams",
                CompleteName = "Ralph Vaughan Williams",
                BirthDate = DateTime.Parse("1872-01-01"),
                DeathDate = DateTime.Parse("1958-01-01"),
                Era = "Late Romantic",
                PortraitLink = "https://assets.openopus.org/portraits/72161419-1568084957.jpg"
            },
            new Composer
            {
                Id = 87,
                Name = "Bach",
                CompleteName = "Johann Sebastian Bach",
                BirthDate =DateTime.Parse("1685-01-01"),
                DeathDate = DateTime.Parse("1750-01-01"),
                Era = "Baroque",
                PortraitLink = "https://assets.openopus.org/portraits/12091447-1568084857.jpg"
            },
            new Composer
            {
                Id = 176,
                Name = "Reich",
                CompleteName = "Steve Reich",
                BirthDate = DateTime.Parse("1936-01-01"),
                DeathDate = null,
                Era = "Post-War",
                PortraitLink = "https://assets.openopus.org/portraits/65680484-1568084938.jpg"
            },
            new Composer
            {
                Id = 196,
                Name = "Mozart",
                CompleteName = "Wolfgang Amadeus Mozart",
                BirthDate = DateTime.Parse("1756-01-01"),
                DeathDate = DateTime.Parse("1791-01-01"),
                Era = "Classical",
                PortraitLink = "https://assets.openopus.org/portraits/21459195-1568084925.jpg"
            }
        };

        public static readonly List<Work> Works = new()
        {
            new Work
            {
                Id = 1,
                ComposerId = 176,
                Title = "3 Movements",
                Subtitle = "",
                Genre = "Orchestral"
            },
            new Work
            {
                Id = 2,
                ComposerId = 202,
                Title = "3 Mouvements perp\u00e9tuels, FP14",
                Subtitle = "",
                Genre = "Keyboard"
            }
        };
    }
}
