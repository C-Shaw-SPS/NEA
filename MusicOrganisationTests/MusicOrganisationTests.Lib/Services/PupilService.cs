using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Enums;
using MusicOrganisationTests.Lib.Models;

namespace MusicOrganisationTests.Lib.Services
{
    public class PupilService : Service<Pupil>
    {
        private Service<Repertoire> _repertoireTable;
        private Service<CaregiverMap> _caregiverMapTable;
        private Service<Caregiver> _caregiverTable;

        public PupilService(string path) : base(path)
        {
            _repertoireTable = new(path);
            _caregiverMapTable = new(path);
            _caregiverTable = new(path);
        }

        public async Task Add(string name, string level, Day lessonDays, bool hasDifferentTimes, string? email, string? phoneNumber)
        {
            int id = await GetNextIdAsync();
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
            await InsertAsync(pupil);
        }

        public async Task<IEnumerable<Caregiver>> GetCaregiversAsync(int pupilId)
        {
            throw new NotImplementedException();
        }
    }
}