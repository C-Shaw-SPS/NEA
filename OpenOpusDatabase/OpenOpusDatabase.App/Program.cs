using OpenOpusDatabase.Lib.APIFetching;
using OpenOpusDatabase.Lib.Databases;
using OpenOpusDatabase.Lib.Models;
using System.Diagnostics;

namespace OpenOpusDatabase.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            await CreateComposerDatabase();
            Console.WriteLine("Composer database created");
            await CreateWorkDatabase();
            Console.WriteLine("Work database created");
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }

        static async Task CreateComposerDatabase()
        {
            IEnumerable<Composer> composers = ComposerGetter.GetFromOpenOpus();
            Database<Composer> composerDatabase = new(DatabaseProperties.NAME);
            await composerDatabase.ClearAsync();
            await composerDatabase.InsertAllAsync(composers);
        }

        static async Task CreateWorkDatabase()
        {
            IEnumerable<Work> works = WorkGetter.GetFromOpenOpus();
            Database<Work> workDatabase = new(DatabaseProperties.NAME);
            await workDatabase.ClearAsync();
            await workDatabase.InsertAllAsync(works);
        }
    }
}