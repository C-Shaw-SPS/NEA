using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ISqlQuery GetSelectAllQuery()
        {
            SqlQuery<WorkData> query = new();
            query.AddColumn<WorkData>(nameof(WorkData.Id), nameof(WorkId));
            query.AddColumn<WorkData>(nameof(WorkData.ComposerId), nameof(ComposerId));
            query.AddColumn<WorkData>(nameof(WorkData.Title), nameof(Title));
            query.AddColumn<WorkData>(nameof(WorkData.Subtitle), nameof(Subtitle));
            query.AddColumn<WorkData>(nameof(WorkData.Genre), nameof(Genre));
            query.AddJoin<ComposerData, WorkData>(nameof(ComposerData.Id), nameof(WorkData.ComposerId));
            query.AddColumn<ComposerData>(nameof(ComposerData.Name), nameof(ComposerName));
            return query;
        }
    }
}