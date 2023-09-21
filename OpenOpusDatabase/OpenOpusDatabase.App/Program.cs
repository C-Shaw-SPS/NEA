using OpenOpusDatabase.Lib.APIFetching;
using OpenOpusDatabase.Lib.Databases;
using OpenOpusDatabase.Lib.Models;

namespace OpenOpusDatabase.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await CreateComposerDatabase();
            Console.WriteLine("Composer database created");
            await CreateWorkDatabase();
            Console.WriteLine("Work database created");
        }

        static async Task CreateComposerDatabase()
        {
            List<Composer> composers = ComposerGetter.GetFromOpenOpus();
            Database<Composer> composerDatabase = new(DatabaseProperties.NAME);
            await composerDatabase.ClearAsync();
            await composerDatabase.InsertAllAsync(composers);
        }

        static async Task CreateWorkDatabase()
        {
            List<Work> works = WorkGetter.GetFromOpenOpus();
            Database<Work> workDatabase = new(DatabaseProperties.NAME);
            await workDatabase.ClearAsync();
            await workDatabase.InsertAllAsync(works);
        }
    }
}