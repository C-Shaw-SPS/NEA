using MusicOrganisationTests.Lib.Json;
using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            await CreateEmptyDatabase();
        }

        static async Task CreateEmptyDatabase()
        {
            await CreateAndInitTable<CaregiverData>();
            await CreateAndInitTable<CaregiverMap>();
            await CreateAndInitTable(ComposerGetter.GetFromOpenOpus());
            await CreateAndInitTable<FixedLessonData>();
            await CreateAndInitTable<LessonData>();
            await CreateAndInitTable<LessonRestrictionData>();
            await CreateAndInitTable<LessonTimeData>();
            await CreateAndInitTable<PupilData>();
            await CreateAndInitTable<RepertoireData>();
            await CreateAndInitTable(WorkGetter.GetFromOpenOpus());
        }

        static async Task CreateAndInitTable<T>(IEnumerable<T>? data = null) where T : class, ITable, new()
        {
            Service tableConnection = new(DatabaseProperties.NAME);
            await tableConnection.InitAsync<T>();
            if (data is not null)
            {
                await tableConnection.InsertAllAsync(data);
            }
        }
    }
}