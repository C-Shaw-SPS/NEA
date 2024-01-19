using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Tables;

namespace MusicOrganisation.Lib.Models
{
    public class Work
    {
        private int _workId;
        private int _composerId;
        private string _title = string.Empty;
        private string _subtitle = string.Empty;
        private string _genre = string.Empty;
        private string _composerName = string.Empty;

        public int WorkId
        {
            get => _workId;
            set => _workId = value;
        }

        public int ComposerId
        {
            get => _composerId;
            set => _composerId = value;
        }

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public string Subtitle
        {
            get => _subtitle;
            set => _subtitle = value;
        }

        public string Genre
        {
            get => _genre;
            set => _genre = value;
        }

        public string ComposerName
        {
            get => _composerName;
            set => _composerName = value;
        }

        public static SqlQuery GetSelectAllQuery()
        {
            SqlQuery<WorkData> query = new(SqlQuery.DEFAULT_LIMIT);
            query.AddColumn<WorkData>(nameof(WorkData.Id), nameof(WorkId));
            query.AddColumn<WorkData>(nameof(WorkData.ComposerId), nameof(ComposerId));
            query.AddColumn<WorkData>(nameof(WorkData.Title), nameof(Title));
            query.AddColumn<WorkData>(nameof(WorkData.Subtitle), nameof(Subtitle));
            query.AddColumn<WorkData>(nameof(WorkData.Genre), nameof(Genre));
            query.AddJoin<ComposerData, WorkData>(nameof(ComposerData.Id), nameof(WorkData.ComposerId));
            query.AddColumn<ComposerData>(nameof(ComposerData.Name), nameof(ComposerName));
            query.AddWhereEquals<WorkData>(nameof(WorkData.IsDeleted), false);
            return query;
        }

        public static SqlQuery GetSelectFromComposerIdQuery(int composerId)
        {
            SqlQuery query = GetSelectAllQuery();
            query.AddWhereEquals<ComposerData>(nameof(ComposerData.Id), composerId);
            return query;
        }

        public UpdateStatement GetUpdateStatement()
        {
            UpdateStatement<WorkData> updateStatement = new(_workId);
            updateStatement.AddColumnToUpdate(nameof(WorkData.ComposerId), _composerId);
            updateStatement.AddColumnToUpdate(nameof(WorkData.Title), _title);
            updateStatement.AddColumnToUpdate(nameof(WorkData.Subtitle), _subtitle);
            updateStatement.AddColumnToUpdate(nameof(WorkData.Genre), _genre);
            return updateStatement;
        }
    }
}