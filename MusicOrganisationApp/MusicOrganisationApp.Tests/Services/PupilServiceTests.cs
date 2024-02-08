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
            await database.ResetTableAsync(ExpectedService.Pupils);
            await database.ResetTableAsync(ExpectedService.RepertoireDatas);
            await database.ResetTableAsync(ExpectedService.CaregiverMaps);
            await database.ResetTableAsync(ExpectedService.LessonDatas);
            await database.ResetTableAsync(ExpectedService.PupilAvaliabilities);

            Pupil pupilToDelete = ExpectedService.Pupils[0];

            PupilService pupilService = new(database);
            await pupilService.DeleteAsync(pupilToDelete);

            await AssertDoesNotContainPupilId<RepertoireData>(database, pupilToDelete.Id);
            await AssertDoesNotContainPupilId<CaregiverMap>(database, pupilToDelete.Id);
            await AssertDoesNotContainPupilId<LessonData>(database, pupilToDelete.Id);
            await AssertDoesNotContainPupilId<PupilAvailability>(database, pupilToDelete.Id);
        }

        private static IEnumerable<T> GetDataToDelete<T>(IEnumerable<T> values, int pupilId) where T : IPupilIdentifiable
        {
            return from value in values
                   where value.PupilId == pupilId
                   select value;
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