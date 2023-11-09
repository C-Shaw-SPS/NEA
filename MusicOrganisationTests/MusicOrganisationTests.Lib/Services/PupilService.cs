using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Enums;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.Lib.Services
{
    public class PupilService : Service
    {
        public PupilService(string path) : base(path)
        {

        }

        public async Task InsertPupilAsync(string name, string level, Day lessonDays, bool hasDifferentTimes, string? email, string? phoneNumber)
        {
            int id = await GetNextIdAsync<PupilData>();
            PupilData pupil = new()
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

        public async Task InsertNewCaregiverAsync(string name, string? email, string? phoneNumber, int pupilId, string description)
        {
            int caregiverId = await GetNextIdAsync<CaregiverData>();

            CaregiverData caregiver = new()
            {
                Id = caregiverId,
                Name = name,
                Email = email,
                PhoneNumber = phoneNumber
            };

            await InsertAsync(caregiver);
            await InsertExistingCaregiverAsync(pupilId, caregiverId, description);
        }

        public async Task InsertExistingCaregiverAsync(int pupilId, int caregiverId, string description)
        {
            int id = await GetNextIdAsync<CaregiverMap>();
            CaregiverMap caregiverMap = new()
            {
                Id = id,
                PupilId = pupilId,
                CaregiverId = caregiverId,
                Description = description
            };

            await InsertAsync(caregiverMap);
        }

        public async Task<IEnumerable<CaregiverData>> GetCaregiversAsync(int pupilId)
        {
            await InitAsync<CaregiverData>();
            await InitAsync<CaregiverMap>();
            string query = $"""
                SELECT {ITable.GetColumnNamesWithTableName<CaregiverData>().CommaJoin()}
                FROM {CaregiverData.TableName}
                JOIN {CaregiverMap.TableName}
                ON {CaregiverData.TableName}.{nameof(CaregiverData.Id)} = {nameof(CaregiverMap)}.{nameof(CaregiverMap.CaregiverId)}
                WHERE {CaregiverMap.TableName}.{nameof(CaregiverMap.PupilId)} = {pupilId}
                """;

            IEnumerable<CaregiverData> result = await _connection.QueryAsync<CaregiverData>(query);
            return result;
        }

        public async Task<IEnumerable<WorkData>> GetRepertoireAsync(int pupilId)
        {
            throw new NotImplementedException();
        }
    }
}