using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Enums;
using MusicOrganisationTests.Lib.Models;
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

        public async Task<IEnumerable<Caregiver>> GetCaregiversAsync(int pupilId)
        {
            await InitAsync<CaregiverData>();
            await InitAsync<CaregiverMap>();
            string query = GetCaregiverQuery(pupilId);
            IEnumerable<Caregiver> result = await _connection.QueryAsync<Caregiver>(query);
            return result;
        }

        public static string GetCaregiverQuery(int pupilId)
        {
            string query = $"""
                SELECT
                {CaregiverMap.TableName}.{nameof(CaregiverMap.Id)} AS {nameof(Caregiver.MapId)},
                {CaregiverMap.TableName}.{nameof(CaregiverMap.Description)} AS {nameof(Caregiver.Description)},
                {CaregiverData.TableName}.{nameof(CaregiverData.Id)} AS {nameof(Caregiver.CaregiverId)},
                {CaregiverData.TableName}.{nameof(CaregiverData.Name)} AS {nameof(Caregiver.Name)},
                {CaregiverData.TableName}.{nameof(CaregiverData.Email)} AS {nameof(Caregiver.Email)},
                {CaregiverData.TableName}.{nameof(CaregiverData.PhoneNumber)} AS {nameof(Caregiver.PhoneNumber)}
                FROM {CaregiverData.TableName}
                JOIN {CaregiverMap.TableName}
                ON {CaregiverData.TableName}.{nameof(CaregiverData.Id)} = {nameof(CaregiverMap)}.{nameof(CaregiverMap.CaregiverId)}
                WHERE {CaregiverMap.TableName}.{nameof(CaregiverMap.PupilId)} = {pupilId}
                """;
            return query;
        }

        public async Task<IEnumerable<Repertoire>> GetRepertoireAsync(int pupilId)
        {
            await InitAsync<RepertoireData>();
            await InitAsync<WorkData>();
            await InitAsync<ComposerData>();
            string query = GetRepertoireQuery(pupilId);
            IEnumerable<Repertoire> repertoires = await _connection.QueryAsync<Repertoire>(query);
            return repertoires;
        }

        private static string GetRepertoireQuery(int pupilId)
        {
            string query = $"""
                SELECT
                {RepertoireData.TableName}.{nameof(RepertoireData.Id)} AS {nameof(Repertoire.RepertoireId)},
                {RepertoireData.TableName}.{nameof(RepertoireData.DateStarted)} AS {nameof(Repertoire.DateStarted)},
                {RepertoireData.TableName}.{nameof(RepertoireData.Syllabus)} AS {nameof(Repertoire.Syllabus)},
                {RepertoireData.TableName}.{nameof(RepertoireData.Status)} AS {nameof(Repertoire.Status)},
                {WorkData.TableName}.{nameof(WorkData.Id)} AS {nameof(Repertoire.WorkId)},
                {WorkData.TableName}.{nameof(WorkData.Title)} AS {nameof(Repertoire.Title)},
                {WorkData.TableName}.{nameof(WorkData.Subtitle)} AS {nameof(Repertoire.Subtitle)},
                {WorkData.TableName}.{nameof(WorkData.Genre)} AS {nameof(Repertoire.Genre)},
                {ComposerData.TableName}.{nameof(ComposerData.Id)} AS {nameof(Repertoire.ComposerId)},
                {ComposerData.TableName}.{nameof(ComposerData.CompleteName)} AS {nameof(Repertoire.ComposerName)}
                FROM {RepertoireData.TableName}
                JOIN {WorkData.TableName}
                ON {RepertoireData.TableName}.{nameof(RepertoireData.WorkId)} = {WorkData.TableName}.{nameof(WorkData.Id)}
                JOIN {ComposerData.TableName}
                ON {WorkData.TableName}.{nameof(WorkData.ComposerId)} = {ComposerData.TableName}.{nameof(ComposerData.Id)}
                WHERE {nameof(RepertoireData.PupilId)} = {pupilId}
                """;
            return query;
        }
    }
}