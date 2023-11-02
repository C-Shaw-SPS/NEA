using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Enums;
using MusicOrganisationTests.Lib.Models;

namespace MusicOrganisationTests.Lib.Services
{
    public class PupilService : Service<Pupil>
    {
        private TableConnection<Repertoire> _repertoireTable;
        private TableConnection<CaregiverMap> _caregiverMapTable;
        private TableConnection<Caregiver> _caregiverTable;

        public PupilService(string path) : base(path)
        {
            _repertoireTable = new(path);
            _caregiverMapTable = new(path);
            _caregiverTable = new(path);
        }

        public async Task InitAsync()
        {
            await _table.InitAsync();
            await _repertoireTable.InitAsync();
            await _caregiverMapTable.InitAsync();
            await _caregiverTable.InitAsync();
        }

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

        public async Task<IEnumerable<Caregiver>> GetCaregiversAsync(int pupilId)
        {
            throw new NotImplementedException();
        }
    }
}