﻿using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Exceptions;
using MusicOrganisation.Lib.Tables;

namespace MusicOrganisation.Lib.Services
{
    public class LessonSlotService : DatabaseConnection
    {
        private const int _MAX_FLAG_INDEX = 32;

        public LessonSlotService(string path) : base(path)
        {
        }

        public async Task<int> GetNextFlagIndexAsync(DayOfWeek dayOfWeek)
        {
            IEnumerable<LessonSlotData> lessonSlotData = await GetWhereEqualAsync<LessonSlotData>(nameof(LessonSlotData.DayOfWeek), dayOfWeek);
            HashSet<int> flagIndexes = lessonSlotData.Select(data => data.FlagIndex).ToHashSet();
            for (int newIndex = 0; newIndex < _MAX_FLAG_INDEX; ++newIndex)
            {
                if (!flagIndexes.Contains(newIndex))
                {
                    return newIndex;
                }
            }
            throw new NoMoreFlagsException(dayOfWeek);
        }
    }
}