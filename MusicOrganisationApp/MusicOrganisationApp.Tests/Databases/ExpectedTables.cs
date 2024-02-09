using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.Models;

namespace MusicOrganisationApp.Tests.Databases
{
    internal static class ExpectedTables
    {
        public static readonly Composer NullPropertyComposer = new()
        {
            Id = 91,
            Name = "Kenji Bunch",
            BirthYear = 1973,
            DeathYear = null,
            Era = "21st Century",
        };

        public static readonly Pupil SqlInjectionPupil = new()
        {
            Id = 0,
            Name = $"Robert'); DROP TABLE {Pupil.TableName}"
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
                Name = "Caregiver 1"
            }
        };

        public static readonly List<CaregiverMap> CaregiverMaps = new()
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
            NullPropertyComposer
        };


        public static readonly List<LessonSlotData> LessonSlotData = new()
        {
            new LessonSlotData
            {
                Id = 0,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(13, 00, 00),
                EndTime = new TimeSpan(14, 00, 00)
            }
        };

        public static readonly List<LessonData> LessonData = new()
        {
            new LessonData
            {
                Id = 0,
                PupilId = 0,
                StartTime = new TimeSpan(09, 00, 00),
                EndTime = new TimeSpan(10, 00, 00),
                Date = DateTime.Parse("30/10/2023"),
                Notes = "notes.txt"
            },
            new LessonData
            {
                Id = 1,
                PupilId = 1,
                StartTime = new TimeSpan(10, 00, 00),
                EndTime = new TimeSpan(10, 30, 00),
                Date = DateTime.Now
            }
        };


        public static readonly List<Pupil> Pupils = new()
        {
            new Pupil
            {
                Id = 0,
                Name = "Pupil 0",
                Level = "Grade 1",
                NeedsDifferentTimes = true,
                LessonDuration = TimeSpan.FromHours(1),
                PhoneNumber = "0123456789",
                Email = "pupil0@email.com"
            },
            new Pupil
            {
                Id = 1,
                Name = "Pupil 1",
                Level = "Grade 8",
                NeedsDifferentTimes = false,
                LessonDuration = TimeSpan.FromMinutes(30)
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
                IsFinishedLearning = true
            },
            new RepertoireData
            {
                Id = 1,
                PupilId = 1,
                WorkId = 1,
                DateStarted = DateTime.Now,
                IsFinishedLearning = false
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

        public static readonly PupilCaregiver Caregiver = new()
        {
            CaregiverId = CaregiverMaps[0].Id,
            Id = CaregiverMaps[0].CaregiverId,
            Description = CaregiverMaps[0].Description,
            Name = CaregiverData[0].Name,
            Email = CaregiverData[0].Email,
            PhoneNumber = CaregiverData[0].PhoneNumber
        };

        public static readonly List<PupilAvailability> PupilAvaliabilities = new()
        {
            new()
            {
                Id = 0,
                PupilId = 0,
                LessonSlotId = 0
            },
            new()
            {
                Id = 1,
                PupilId = 1,
                LessonSlotId = 1
            }
        };
    }
}