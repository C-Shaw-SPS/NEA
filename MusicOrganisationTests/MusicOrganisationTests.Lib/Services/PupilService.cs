using MusicOrganisationTests.Lib.Enums;
using MusicOrganisationTests.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Lib.Services
{
    public class PupilService : Service<Pupil>
    {
        public PupilService(string path) : base(path) { }

        public async Task Add(string name, string level, Day lessonDays, bool hasDifferentTimes, string? email, string? phoneNumber)
        {
            int id = await _table.GetNextIdAsync();
            Pupil pupil = new()
            {
                Id = id,
                Name = name,
                Level = level,
                LessonDays = lessonDays,
                HasDifferentTimes = hasDifferentTimes,
                Email = email,
                PhoneNumber = phoneNumber
            };
            await _table.InsertAsync(pupil);
        }
    }
}