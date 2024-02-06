using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Services
{
    public class PupilCaregiverServiceTests
    {
        [Fact]
        public async Task TestInsertCaregiverAsync()
        {
            (DatabaseConnection database, PupilCaregiverService service) = await GetDatabaseAndService(nameof(TestInsertCaregiverAsync), false);
            PupilCaregiver expectedCaregiver = ExpectedService.Caregivers[0];
            await service.InsertAsync(expectedCaregiver, false);
            IEnumerable<PupilCaregiver> actualCaregivers = await service.GetAllAsync();
            Assert.Single(actualCaregivers);
            Assert.Contains(expectedCaregiver, actualCaregivers);
        }

        [Fact]
        public async Task TestGetPupilCaregiversAsync()
        {
            (DatabaseConnection database, PupilCaregiverService service) = await GetDatabaseAndService(nameof(TestGetPupilCaregiversAsync), true);
            Pupil expectedPupil = ExpectedService.Pupils[0];
            service.PupilId = expectedPupil.Id;
            IEnumerable<PupilCaregiver> expectedCaregivers = ExpectedService.Caregivers.Where(c => c.PupilId == expectedPupil.Id);
            IEnumerable<PupilCaregiver> actualCaregivers = await service.SearchAsync(string.Empty, nameof(PupilCaregiver.Id));
            CollectionAssert.Equal(expectedCaregivers, actualCaregivers);
        }

        private static async Task<(DatabaseConnection database, PupilCaregiverService service)> GetDatabaseAndService(string path, bool insertCaregiverMaps)
        {
            DatabaseConnection database = new(path);
            PupilCaregiverService service = new(database);
            await database.ResetTableAsync(ExpectedService.CaregiverDatas);
            await database.ResetTableAsync(ExpectedService.Pupils);
            await database.DropTableIfExistsAsync<CaregiverMap>();
            if (insertCaregiverMaps)
            {
                await database.InsertAllAsync(ExpectedService.CaregiverMaps);
            }
            return (database, service);
        }
    }
}