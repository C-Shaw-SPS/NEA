using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class PupilService : IService<Pupil>
    {
        private readonly DatabaseConnection _database;

        public PupilService(DatabaseConnection database)
        {
            _database = database;
        }

        public async Task DeleteAsync(Pupil value)
        {
            await _database.DeleteAsync(value);
            await DeletePupilData<RepertoireData>(value.Id);
            await DeletePupilData<CaregiverMap>(value.Id);
            await DeletePupilData<LessonData>(value.Id);
            await DeletePupilData<PupilLessonSlotData>(value.Id);
        }


        private async Task DeletePupilData<T>(int pupilId) where T: class, IPupilIdentifiable, ITable, new()
        {
            DeleteStatement<T> deleteStatement = new();
            deleteStatement.AddCondition(nameof(IPupilIdentifiable.PupilId), pupilId);
            await _database.ExecuteAsync(deleteStatement);
        }

        public Task<IEnumerable<Pupil>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Pupil value, bool getNewId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pupil>> SearchAsync(string search, string ordering)
        {
            throw new NotImplementedException();
        }

        public Task<(bool, Pupil)> TryGetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Pupil value)
        {
            throw new NotImplementedException();
        }
    }
}
