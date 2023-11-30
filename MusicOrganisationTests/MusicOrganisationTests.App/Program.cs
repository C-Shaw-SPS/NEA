using MusicOrganisationTests.Lib.Json;
using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Tables;
using MusicOrganisationTests.Lib.Services;
using System.Reflection.Metadata;

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
            await Task.WhenAll(
                CreateAndInitTable<CaregiverData>(),
                CreateAndInitTable<CaregiverMap>(),
                CreateAndInitTable(ComposerGetter.GetFromOpenOpus()),
                CreateAndInitTable<FixedLessonData>(),
                CreateAndInitTable<LessonData>(),
                CreateAndInitTable(DefaultValues.LessonSlotData),
                CreateAndInitTable(DefaultValues.PupilData),
                CreateAndInitTable<RepertoireData>(),
                CreateAndInitTable(WorkGetter.GetFromOpenOpus())
            );
        }

        static async Task CreateAndInitTable<T>(IEnumerable<T>? data = null) where T : class, ITable, new()
        {
            Service service = new(DatabaseProperties.NAME);
            await service.ClearTableAsync<T>();
            if (data is not null)
            {
                await service.InsertAllAsync(data);
            }
        }
    }
}