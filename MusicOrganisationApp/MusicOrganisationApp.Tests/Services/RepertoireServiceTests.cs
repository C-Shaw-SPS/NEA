using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Services
{
    public class RepertoireServiceTests
    {
        [Fact]
        public async Task TestInsertRepertoireAsync()
        {
            (DatabaseConnection database, RepertoireService service) = await GetDatabaseAndService(nameof(TestInsertRepertoireAsync), false);

            Repertoire expectedRepertoire = ExpectedService.Repertoires[0];
            await service.InsertAsync(expectedRepertoire, false);
            IEnumerable<Repertoire> actualRepertoires = await service.GetAllAsync();

            Assert.Single(actualRepertoires);
            Assert.Contains(expectedRepertoire, actualRepertoires);
        }

        [Fact]
        public async Task TestGetAllRepertoiresAsync()
        {
            (DatabaseConnection dataase, RepertoireService service) = await GetDatabaseAndService(nameof(TestGetAllRepertoiresAsync), true);
            IEnumerable<Repertoire> actualRepertoires = await service.GetAllAsync();
            CollectionAssert.Equal(ExpectedService.Repertoires, actualRepertoires);
        }

        [Fact]
        public async Task TestGetPupilRepertoiresAsync()
        {
            (DatabaseConnection database, RepertoireService service) = await GetDatabaseAndService(nameof(TestGetAllRepertoiresAsync), true);
            int pupilId = ExpectedService.Repertoires[0].PupilId;
            service.PupilId = pupilId;
            IEnumerable<Repertoire> expectedRepertoires = ExpectedService.Repertoires.Where(r => r.PupilId == pupilId);
            IEnumerable<Repertoire> actualRepertoires = await service.SearchAsync(string.Empty, nameof(Repertoire.Title));
            CollectionAssert.Equal(expectedRepertoires, actualRepertoires);
        }

        private static async Task<(DatabaseConnection database, RepertoireService service)> GetDatabaseAndService(string path, bool insertRepertoireData)
        {
            DatabaseConnection database = new(path);
            RepertoireService service = new(database);
            await database.ResetTableAsync(ExpectedService.Composers);
            await database.ResetTableAsync(ExpectedService.WorkDatas);
            await database.DropTableIfExistsAsync<RepertoireData>();

            if (insertRepertoireData)
            {
                await database.InsertAllAsync(ExpectedService.RepertoireDatas);
            }

            return (database, service);
        }
    }
}