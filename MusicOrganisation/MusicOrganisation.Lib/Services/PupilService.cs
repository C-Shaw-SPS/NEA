using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Models;

namespace MusicOrganisation.Lib.Services
{
    public class PupilService : Service
    {
        public PupilService(string path) : base(path)
        {

        }

        public async Task InsertPupilAsync(string name, string level, bool hasDifferentTimes, TimeSpan lessonDuration, int mondayLessonSlots, int tuesdayLessonSlots, int wednesdayLessonSlots, int thursdayLessonSlots, int fridayLessonSlots, int saturdayLessonSlots, int sundayLessonSlots, string? email, string? phoneNumber)
        {
            int id = await GetNextIdAsync<Pupil>();
            Pupil pupil = new()
            {
                Id = id,
                Name = name,
                Level = level,
                NeedsDifferentTimes = hasDifferentTimes,
                LessonDuration = lessonDuration,
                MondayLessonSlots = mondayLessonSlots,
                TuesdayLessonSlots = tuesdayLessonSlots,
                WednesdayLessonSlots = wednesdayLessonSlots,
                ThursdayLessonSlots = thursdayLessonSlots,
                FridayLessonSlots = fridayLessonSlots,
                SaturdayLessonSlots = saturdayLessonSlots,
                SundayLessonSlots = sundayLessonSlots,
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
            SqlQuery<CaregiverData> query = new();
            query.AddColumn<CaregiverMap>(nameof(CaregiverMap.Id), nameof(Caregiver.MapId));
            query.AddColumn<CaregiverMap>(nameof(CaregiverMap.Description), nameof(Caregiver.Description));
            query.AddColumn<CaregiverData>(nameof(CaregiverData.Id), nameof(Caregiver.CaregiverId));
            query.AddColumn<CaregiverData>(nameof(CaregiverData.Name), nameof(Caregiver.Name));
            query.AddColumn<CaregiverData>(nameof(CaregiverData.Email), nameof(Caregiver.Email));
            query.AddColumn<CaregiverData>(nameof(CaregiverData.PhoneNumber), nameof(Caregiver.PhoneNumber));
            query.AddJoin<CaregiverMap, CaregiverData>(nameof(CaregiverMap.CaregiverId), nameof(CaregiverData.Id));
            query.AddWhereEquals<CaregiverMap>(nameof(CaregiverMap.PupilId), pupilId);

            return query.ToString();
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
            SqlQuery<RepertoireData> query = new();

            query.AddColumn<RepertoireData>(nameof(RepertoireData.Id), nameof(Repertoire.RepertoireId));
            query.AddColumn<RepertoireData>(nameof(RepertoireData.DateStarted), nameof(Repertoire.DateStarted));
            query.AddColumn<RepertoireData>(nameof(RepertoireData.Syllabus), nameof(Repertoire.Syllabus));
            query.AddColumn<RepertoireData>(nameof(RepertoireData.Status), nameof(Repertoire.Status));
            query.AddColumn<WorkData>(nameof(WorkData.Id), nameof(Repertoire.WorkId));
            query.AddColumn<WorkData>(nameof(WorkData.Title), nameof(Repertoire.Title));
            query.AddColumn<WorkData>(nameof(WorkData.Subtitle), nameof(Repertoire.Subtitle));
            query.AddColumn<WorkData>(nameof(WorkData.Genre), nameof(Repertoire.Genre));
            query.AddColumn<ComposerData>(nameof(ComposerData.Id), nameof(Repertoire.ComposerId));
            query.AddColumn<ComposerData>(nameof(ComposerData.Name), nameof(Repertoire.ComposerName));
            query.AddJoin<WorkData, RepertoireData>(nameof(WorkData.Id), nameof(RepertoireData.WorkId));
            query.AddJoin<ComposerData, WorkData>(nameof(ComposerData.Id), nameof(WorkData.ComposerId));
            query.AddWhereEquals<RepertoireData>(nameof(RepertoireData.PupilId), pupilId);

            return query.ToString();
        }
    }
}