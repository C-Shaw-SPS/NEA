using OpenOpusDatabase.Lib.Models;

namespace OpenOpusDatabase.Lib.Databases
{
    public class ComposerDatabase : Database<Composer>
    {
        public ComposerDatabase(string path) : base(path)
        {

        }
    }
}