using MusicOrganisationTests.Lib.Services;
using MusicOrganisationTests.Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Lib.Models
{
    public class Pupil
    {
        private int _id;
        private string _name;
        private string _level;
        private bool _hasDifferentTimes;
        private TimeSpan _lessonDuration;
        private Dictionary<DayOfWeek, int> _lessonSlots;
        private string? _email;
        private string? _phoneNumber;
        private string? _notesFile;

        public Pupil(PupilData pupilData)
        {
            _id = pupilData.Id;
            _name = pupilData.Name;
            _level = pupilData.Level;
            _hasDifferentTimes = pupilData.HasDifferentTimes;
            _lessonDuration = pupilData.LessonDuration;
            _lessonSlots = GetLessonSlots(
                pupilData.MondayLessonSlots,
                pupilData.TuesdayLessonSlots,
                pupilData.WednesdayLessonSlots,
                pupilData.ThursdayLessonSlots,
                pupilData.FridayLessonSlots,
                pupilData.SaturdayLessonSlots,
                pupilData.SundayLessonSlots
                );
            _email = pupilData.Email;
            _phoneNumber = pupilData.PhoneNumber;
            _notesFile = pupilData.NotesFile;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Level
        {
            get => _level;
            set => _level = value;
        }

        public bool HasDifferentTimes
        {
            get => _hasDifferentTimes;
            set => _hasDifferentTimes = value;
        }

        public TimeSpan LessonDuration
        {
            get => _lessonDuration;
            set => _lessonDuration = value;
        }

        public string? Email
        {
            get => _email;
            set => _email = value;
        }

        public string? PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public string? NotesFile
        {
            get => _notesFile;
            set => _notesFile = value;
        }

        private static Dictionary<DayOfWeek, int> GetLessonSlots(int monday, int tuesday, int wednesday, int thursday, int friday, int saturday, int sunday)
        {
            return new Dictionary<DayOfWeek, int>
            {
                { DayOfWeek.Monday, monday },
                { DayOfWeek.Tuesday, tuesday },
                { DayOfWeek.Wednesday, wednesday },
                { DayOfWeek.Thursday, thursday },
                { DayOfWeek.Friday, friday },
                { DayOfWeek.Saturday, saturday },
                { DayOfWeek.Sunday, sunday },
            };
        }
    }
}