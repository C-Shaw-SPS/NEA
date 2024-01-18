using MusicOrganisation.Lib.Databases;

namespace MusicOrganisation.Lib.Models
{
    public interface IModel
    {
        public ISqlStatement GetSelectAllQuery();

        public ISqlStatement GetUpdateQuery();

        public ISqlStatement GetDeleteQuery();
    }
}