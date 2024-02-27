using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.TestData
{
    public class TestData1
    {
        #region Data

        private static readonly List<LessonSlot> _lessonSlots = new()
        {
            new LessonSlot
            {
                Id = 0,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(09, 00, 00),
                EndTime = new TimeSpan(10, 00, 00)
            },
            new LessonSlot
            {
                Id = 1,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(10, 00, 00),
                EndTime = new TimeSpan(11, 00, 00)
            },
            new LessonSlot
            {
                Id = 2,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(11, 00, 00),
                EndTime = new TimeSpan(12, 00, 00)
            },
            new LessonSlot
            {
                Id = 3,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(12, 00, 00),
                EndTime = new TimeSpan(13, 00, 00)
            },
            new LessonSlot
            {
                Id = 4,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(13, 00, 00),
                EndTime = new TimeSpan(14, 00, 00)
            },
            new LessonSlot
            {
                Id = 5,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(14, 00, 00),
                EndTime = new TimeSpan(15, 00, 00)
            },
            new LessonSlot
            {
                Id = 6,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(15, 00, 00),
                EndTime = new TimeSpan(16, 00, 00)
            },
            new LessonSlot
            {
                Id = 7,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(16, 00, 00),
                EndTime = new TimeSpan(17, 00, 00)
            }
        };

        private static readonly List<Pupil> _pupilData = new()
        {
            new Pupil
            {
                Id = 0,
                NeedsDifferentTimes = false,
                LessonDuration = new TimeSpan(01, 00, 00),

            },
            new Pupil
            {
                Id = 1,
                NeedsDifferentTimes = false,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 2,
                NeedsDifferentTimes = false,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 3,
                NeedsDifferentTimes = false,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 4,
                NeedsDifferentTimes = true,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 5,
                NeedsDifferentTimes = true,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 6,
                NeedsDifferentTimes = true,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 7,
                NeedsDifferentTimes = true,
                LessonDuration = new TimeSpan(01, 00, 00)
            }
        };

        private static readonly List<Pupil> _pupils = GetPupils();

        private static readonly List<LessonData> _lessons = new()
        {
            new LessonData
            {
                Id = 0,
                PupilId = _pupilData[0].Id,
                StartTime = _lessonSlots[0].StartTime,
                EndTime = _lessonSlots[0].EndTime,
                Date = DateGetter.FromDayOfWeek(DateTime.Today, _lessonSlots[0].DayOfWeek)
            },
            new LessonData
            {
                Id = 1,
                PupilId = _pupilData[1].Id,
                StartTime = _lessonSlots[1].StartTime,
                EndTime = _lessonSlots[1].EndTime,
                Date = DateGetter.FromDayOfWeek(DateTime.Today, _lessonSlots[1].DayOfWeek)
            },
            new LessonData
            {
                Id = 2,
                PupilId = _pupilData[2].Id,
                StartTime = _lessonSlots[2].StartTime,
                EndTime = _lessonSlots[2].EndTime,
                Date = DateGetter.FromDayOfWeek(DateTime.Today, _lessonSlots[2].DayOfWeek)
            },
            new LessonData
            {
                Id = 3,
                PupilId = _pupilData[3].Id,
                StartTime = _lessonSlots[3].StartTime,
                EndTime = _lessonSlots[3].EndTime,
                Date = DateGetter.FromDayOfWeek(DateTime.Today, _lessonSlots[3].DayOfWeek)
            },
            new LessonData
            {
                Id = 4,
                PupilId = _pupilData[4].Id,
                StartTime = _lessonSlots[5].StartTime,
                EndTime = _lessonSlots[5].EndTime,
                Date = DateGetter.FromDayOfWeek(DateTime.Today, _lessonSlots[5].DayOfWeek)
            },
            new LessonData
            {
                Id = 5,
                PupilId = _pupilData[5].Id,
                StartTime = _lessonSlots[6].StartTime,
                EndTime = _lessonSlots[6].EndTime,
                Date = DateGetter.FromDayOfWeek(DateTime.Today, _lessonSlots[6].DayOfWeek)
            },
            new LessonData
            {
                Id = 6,
                PupilId = _pupilData[6].Id,
                StartTime = _lessonSlots[7].StartTime,
                EndTime = _lessonSlots[7].EndTime,
                Date = DateGetter.FromDayOfWeek(DateTime.Today, _lessonSlots[7].DayOfWeek)
            },
            new LessonData
            {
                Id = 7,
                PupilId = _pupilData[7].Id,
                StartTime = _lessonSlots[4].StartTime,
                EndTime = _lessonSlots[4].EndTime,
                Date = DateGetter.FromDayOfWeek(DateTime.Today, _lessonSlots[4].DayOfWeek)
            },
        };

        private static readonly List<PupilAvailability> _pupilAvailabilities = new()
        {
            new PupilAvailability
            {
                Id = 0,
                PupilId = _pupilData[0].Id,
                LessonSlotId = _lessonSlots[0].Id
            },
            new PupilAvailability
            {
                Id = 1,
                PupilId = _pupilData[1].Id,
                LessonSlotId = _lessonSlots[1].Id
            },
            new PupilAvailability
            {
                Id = 2,
                PupilId = _pupilData[2].Id,
                LessonSlotId = _lessonSlots[1].Id
            },
            new PupilAvailability
            {
                Id = 3,
                PupilId = _pupilData[2].Id,
                LessonSlotId = _lessonSlots[2].Id
            },
            new PupilAvailability
            {
                Id = 4,
                PupilId = _pupilData[3].Id,
                LessonSlotId = _lessonSlots[2].Id
            },
            new PupilAvailability
            {
                Id = 5,
                PupilId = _pupilData[3].Id,
                LessonSlotId = _lessonSlots[3].Id
            },
            new PupilAvailability
            {
                Id = 6,
                PupilId = _pupilData[4].Id,
                LessonSlotId = _lessonSlots[3].Id
            },
            new PupilAvailability
            {
                Id = 7,
                PupilId = _pupilData[4].Id,
                LessonSlotId = _lessonSlots[4].Id
            },
            new PupilAvailability
            {
                Id = 8,
                PupilId = _pupilData[4].Id,
                LessonSlotId = _lessonSlots[5].Id
            },
            new PupilAvailability
            {
                Id = 9,
                PupilId = _pupilData[5].Id,
                LessonSlotId = _lessonSlots[4].Id
            },
            new PupilAvailability
            {
                Id = 10,
                PupilId = _pupilData[5].Id,
                LessonSlotId = _lessonSlots[5].Id
            },
            new PupilAvailability
            {
                Id = 11,
                PupilId = _pupilData[5].Id,
                LessonSlotId = _lessonSlots[6].Id
            },
            new PupilAvailability
            {
                Id = 12,
                PupilId = _pupilData[6].Id,
                LessonSlotId = _lessonSlots[5].Id
            },
            new PupilAvailability
            {
                Id = 13,
                PupilId = _pupilData[6].Id,
                LessonSlotId = _lessonSlots[6].Id
            },
            new PupilAvailability
            {
                Id = 14,
                PupilId = _pupilData[6].Id,
                LessonSlotId = _lessonSlots[7].Id
            },
            new PupilAvailability
            {
                Id = 15,
                PupilId = _pupilData[7].Id,
                LessonSlotId = _lessonSlots[6].Id
            },
            new PupilAvailability
            {
                Id = 16,
                PupilId = _pupilData[7].Id,
                LessonSlotId = _lessonSlots[7].Id
            },
            new PupilAvailability
            {
                Id = 17,
                PupilId = _pupilData[7].Id,
                LessonSlotId = _lessonSlots[4].Id
            }
        };

        #endregion

        public static List<LessonData> Lessons => _lessons;

        public static List<LessonSlot> LessonSlots => _lessonSlots;

        public static List<Pupil> Pupils => _pupils;

        public static List<PupilAvailability> PupilAvailabilities => _pupilAvailabilities;

        private static List<Pupil> GetPupils()
        {
            foreach (Pupil pupil in _pupilData)
            {
                int id = pupil.Id;
                pupil.Name = $"Pupil {id}";
                pupil.Level = $"Grade {id}";
                pupil.Email = $"pupil{id}@email.com";
                pupil.PhoneNumber = $"000{id}";
                pupil.Notes = $"Notes {id}";
            }
            return _pupilData;
        }

    }
}