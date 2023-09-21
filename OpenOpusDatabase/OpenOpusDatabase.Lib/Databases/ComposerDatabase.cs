using OpenOpusDatabase.Lib.Models;
using System.Data.Common;

namespace OpenOpusDatabase.Lib.Databases
{
    public class ComposerDatabase : Database<Composer>
    {
        public ComposerDatabase(string path) : base(path)
        {

        }

        public override async Task InsertAsync(Composer value)
        {
            await InitAsync();
            string command = $"""
                INSERT INTO {TableName}
                VALUES ({value.Id}, "{value.Name}", "{value.CompleteName}", {value.BirthDate.Ticks}, {value.DeathDate?.Ticks}, "{value.Era}", "{value.PortraitLink}")
                """;
            await Connection.ExecuteAsync(command);
        }
    }
}