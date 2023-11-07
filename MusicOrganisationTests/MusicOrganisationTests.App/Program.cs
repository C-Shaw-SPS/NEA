using MusicOrganisationTests.Lib.APIFetching;
using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Services;
using System.Reflection;

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
            await CreateAndInitTable<Caregiver>();
            await CreateAndInitTable<CaregiverMap>();
            await CreateAndInitTable(ComposerGetter.GetFromOpenOpus());
            await CreateAndInitTable<FixedLesson>();
            await CreateAndInitTable<Lesson>();
            await CreateAndInitTable<LessonRestriction>();
            await CreateAndInitTable<LessonTime>();
            await CreateAndInitTable<Pupil>();
            await CreateAndInitTable<Repertoire>();
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