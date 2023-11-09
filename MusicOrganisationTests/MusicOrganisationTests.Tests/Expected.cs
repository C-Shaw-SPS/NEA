using MusicOrganisationTests.Lib.Enums;
using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.Tests
{
    internal static class Expected
    {
        public static readonly ComposerData NullPropertyComposer = new()
        {
            Id = 91,
            Name = "Bunch",
            CompleteName = "Kenji Bunch",
            BirthDate = DateTime.Parse("1973-07-27"),
            DeathDate = null,
            Era = "21st Century",
            PortraitLink = null
        };

        public static readonly List<CaregiverData> CaregiverData = new()
        {
            new CaregiverData
            {
                Id = 0,
                Name = "Caregiver 0",
                Email = "caregiver0@email.com",
                PhoneNumber = "0123456789"
            },
            new CaregiverData
            {
                Id = 1,
                Name = "Caregiver 1",
                Email = null,
                PhoneNumber = null
            }
        };

        public static readonly List<CaregiverMap> CaregiverMap = new()
        {
            new CaregiverMap
            {
                Id = 0,
                PupilId = 0,
                CaregiverId = 0,
                Description = "Caregiver"
            },
            new CaregiverMap
            {
                Id = 1,
                PupilId = 1,
                CaregiverId = 0
            }
        };

        public static readonly List<ComposerData> ComposerData = new()
        {
            new ComposerData
            {
                Id = 36,
                Name = "Vaughan Williams",
                CompleteName = "Ralph Vaughan Williams",
                BirthDate = DateTime.Parse("1872-01-01"),
                DeathDate = DateTime.Parse("1958-01-01"),
                Era = "Late Romantic",
                PortraitLink = "https://assets.openopus.org/portraits/72161419-1568084957.jpg"
            },
            new ComposerData
            {
                Id = 87,
                Name = "Bach",
                CompleteName = "Johann Sebastian Bach",
                BirthDate =DateTime.Parse("1685-01-01"),
                DeathDate = DateTime.Parse("1750-01-01"),
                Era = "Baroque",
                PortraitLink = "https://assets.openopus.org/portraits/12091447-1568084857.jpg"
            },
            new ComposerData
            {
                Id = 176,
                Name = "Reich",
                CompleteName = "Steve Reich",
                BirthDate = DateTime.Parse("1936-01-01"),
                DeathDate = null,
                Era = "Post-War",
                PortraitLink = "https://assets.openopus.org/portraits/65680484-1568084938.jpg"
            },
            new ComposerData
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

        public static readonly List<FixedLessonData> FixedLessonData = new()
        {
            new FixedLessonData
            {
                Id = 0,
                PupilId = 0,
                LessonTimeId = 0
            }
        };

        public static readonly List<LessonData> LessonData = new()
        {
            new LessonData
            {
                Id = 0,
                PupilId = 0,
                LessonTimeId = 0,
                Date = DateTime.Parse("30/10/2023"),
                NotesFile = "notes.txt"
            },
            new LessonData
            {
                Id = 1,
                PupilId = 1,
                LessonTimeId = 1,
                Date = DateTime.Now
            }
        };

        public static readonly List<LessonRestrictionData> LessonRestrictionData = new()
        {
            new LessonRestrictionData
            {
                Id = 0,
                PupilId = 0,
                Day = Day.Monday,
                StartTime = DateTime.Parse("14:00"),
                EndTime = DateTime.Parse("15:00")
            }
        };

        public static readonly List<LessonTimeData> LessonTimeData = new()
        {
            new LessonTimeData
            {
                Id = 0,
                DayOfWeek = Day.Monday,
                StartTime = DateTime.Parse("13:30"),
                EndTime = DateTime.Parse("14:00")
            },
        };

        public static readonly List<PupilData> PupilData = new()
        {
            new PupilData
            {
                Id = 0,
                Name = "Pupil 0",
                Level = "Grade 1",
                LessonDays = Day.Monday,
                HasDifferentTimes = true,
                PhoneNumber = "0123456789",
                Email = "pupil0@email.com"
            },
            new PupilData
            {
                Id = 1,
                Name = "Pupil 1",
                Level = "Grade 8",
                LessonDays = Day.Tuesday | Day.Wednesday | Day.Friday,
                PhoneNumber = null,
                Email = null
            }
        };

        public static readonly List<RepertoireData> RepertoireData = new()
        {
            new RepertoireData
            {
                Id = 0,
                PupilId = 0,
                WorkId = 0,
                DateStarted = DateTime.Parse("23/10/2023"),
                Syllabus = "Grade 8",
                Status = RepertoireStatus.CurrentlyLearning
            },
            new RepertoireData
            {
                Id = 1,
                PupilId = 1,
                WorkId = 1,
                DateStarted = DateTime.Now,
                Status = RepertoireStatus.FinishedLearning
            }
        };

        public static readonly List<WorkData> WorkData = new()
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

        public static readonly Caregiver Caregiver = new()
        {
            MapId = CaregiverMap[0].Id,
            CaregiverId = CaregiverMap[0].CaregiverId,
            Description = CaregiverMap[0].Description,
            Name = CaregiverData[0].Name,
            Email = CaregiverData[0].Email,
            PhoneNumber = CaregiverData[0].PhoneNumber
        };
    }
}