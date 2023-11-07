using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Databases;

namespace MusicOrganisationTests.Tests.Databases
{
    public class MultipleTablesTests
    {
        [Fact]
        public async void TestMultipleTables()
        {
            Service service = new(nameof(TestMultipleTables));

            await service.ClearDataAsync<Composer>();
            await service.ClearDataAsync<Work>();

            await service.InsertAllAsync(Expected.Composers);
            await service.InsertAllAsync(Expected.Works);

            IEnumerable<Composer> actualComposers = await service.GetAllAsync<Composer>();
            IEnumerable<Work> actualWorks = await service.GetAllAsync<Work>();

            foreach (Composer expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }

            foreach (Work expectedWork in Expected.Works)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }

        [Fact]
        public async void TestClearOneTable()
        {
            Service service = new(nameof(TestClearOneTable));

            await service.ClearDataAsync<Composer>();
            await service.ClearDataAsync<Work>();

            await service.InsertAllAsync(Expected.Composers);
            await service.InsertAllAsync(Expected.Works);

            await service.ClearDataAsync<Composer>();

            IEnumerable<Composer> actualComposers = await service.GetAllAsync<Composer>();
            IEnumerable<Work> actualWorks = await service.GetAllAsync<Work>();

            Assert.Empty(actualComposers);

            foreach (Work expectedWork in Expected.Works)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }
    }
}