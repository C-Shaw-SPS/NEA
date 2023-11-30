using MusicOrganisationTests.Lib.Exceptions;
using MusicOrganisationTests.Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Lib.Services
{
    public class TimetableService : Service
    {
        public TimetableService(string path) : base(path)
        {
        }

        public bool IsValidLessonSlot(LessonSlotData lessonSlotData, PupilData pupilData)
        {
            return IsPupilAvaliable(lessonSlotData, pupilData) && IsLongEnoughTimeSlot(lessonSlotData, pupilData);
        }

        private bool IsPupilAvaliable(LessonSlotData lessonSlotData, PupilData pupilData)
        {
            return lessonSlotData.DayOfWeek switch
            {
                DayOfWeek.Monday => pupilData.MondayLessonSlots.HasFlagAtIndex(lessonSlotData.FlagIndex),
                DayOfWeek.Tuesday => pupilData.TuesdayLessonSlots.HasFlagAtIndex(lessonSlotData.FlagIndex),
                DayOfWeek.Wednesday => pupilData.WednesdayLessonSlots.HasFlagAtIndex(lessonSlotData.FlagIndex),
                DayOfWeek.Thursday => pupilData.ThursdayLessonSlots.HasFlagAtIndex(lessonSlotData.FlagIndex),
                DayOfWeek.Friday => pupilData.FridayLessonSlots.HasFlagAtIndex(lessonSlotData.FlagIndex),
                DayOfWeek.Saturday => pupilData.SaturdayLessonSlots.HasFlagAtIndex(lessonSlotData.FlagIndex),
                DayOfWeek.Sunday => pupilData.SundayLessonSlots.HasFlagAtIndex(lessonSlotData.FlagIndex),
                _ => throw new DayOfWeekException(lessonSlotData.DayOfWeek),
            };
        }

        private bool IsLongEnoughTimeSlot(LessonSlotData lessonSlotData, PupilData pupilData)
        {
            return (lessonSlotData.EndTime - lessonSlotData.StartTime) >= pupilData.LessonDuration;
        }
    }
}
