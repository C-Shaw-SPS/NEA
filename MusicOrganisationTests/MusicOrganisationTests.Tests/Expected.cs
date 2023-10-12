using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Timetabling;

namespace MusicOrganisationTests.Tests
{
    internal static class Expected
    {
        public static readonly Composer NullPropertyComposer = new()
        {
            Id = 91,
            Name = "Bunch",
            CompleteName = "Kenji Bunch",
            BirthDate = DateTime.Parse("1973-07-27"),
            DeathDate = null,
            Era = "21st Century",
            PortraitLink = null
        };

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
            },
            NullPropertyComposer
        };

        public static readonly List<Work> Works = new()
        {
            new Work
            {
                Id = 20086,
                ComposerId = 176,
                Title = "3 Movements",
                Subtitle = "",
                Genre = "Orchestral"
            },
            new Work
            {
                Id = 25115,
                ComposerId = 202,
                Title = "3 Mouvements perp\u00e9tuels, FP14",
                Subtitle = "",
                Genre = "Keyboard"
            }
        };

        public static readonly List<Pupil> Pupils = new()
        {
            new Pupil
            {
                Id = 0,
                Name = "Pupil 0",
                Level = "Grade 1",
                LessonDuration = TimeSpan.FromMinutes(30),
                LessonDay = Day.Monday,
                DifferentTimes = true,
                PhoneNumber = "0123456789",
                Email = "testemail@gmail.com"
            },
            new Pupil
            {
                Id = 1,
                Name = "Pupil 1",
                Level = "Grade 8",
                LessonDuration = TimeSpan.FromHours(2),
                LessonDay = Day.Tuesday | Day.Wednesday | Day.Friday,
                PhoneNumber = null,
                Email = null
            }
        };
    }
}