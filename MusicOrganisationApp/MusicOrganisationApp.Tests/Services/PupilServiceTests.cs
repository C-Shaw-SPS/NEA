using MusicOrganisationApp.Lib;
using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Services
{
    public class PupilServiceTests
    {
        [Fact]
        public async Task TestDeletePupilAsync()
        {
            DatabaseConnection database = new(nameof(TestDeletePupilAsync));
            await database.ResetTableAsync(ExpectedService.Pupils, false);
            await database.ResetTableAsync(ExpectedService.RepertoireDatas, false);
            await database.ResetTableAsync(ExpectedService.CaregiverMaps, false);
            await database.ResetTableAsync(ExpectedService.LessonDatas, false);
            await database.ResetTableAsync(ExpectedService.PupilAvaliabilities, false);

            Pupil pupilToDelete = ExpectedService.Pupils[0];

            PupilService pupilService = new(database);
            await pupilService.DeleteAsync(pupilToDelete);

            await AssertDoesNotContainPupilId<RepertoireData>(database, pupilToDelete.Id);
            await AssertDoesNotContainPupilId<CaregiverMap>(database, pupilToDelete.Id);
            await AssertDoesNotContainPupilId<LessonData>(database, pupilToDelete.Id);
            await AssertDoesNotContainPupilId<PupilAvailability>(database, pupilToDelete.Id);
        }

        private static async Task AssertDoesNotContainPupilId<T>(DatabaseConnection database, int pupilId) where T : class, ITable, IPupilIdentifiable, new()
        {
            IEnumerable<T> values = await database.GetAllAsync<T>();
            foreach (T value in values)
            {
                Assert.NotEqual(pupilId, value.PupilId);
            }
        }
    }
}