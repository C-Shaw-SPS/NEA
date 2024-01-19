﻿using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Json;
using MusicOrganisation.Lib.Databases;

namespace MusicOrganisation.Lib.Services
{
    public class WorkService : Database
    {
        public WorkService(string path) : base(path) { }

        public async Task InitialiseData()
        {
            IEnumerable<WorkData> works = await WorkGetter.GetFromOpenOpus();
            await InsertAllAsync(works);
        }

        public async Task AddWork(int composerId, string title, string subtitle, string genre)
        {
            int id = await GetNextIdAsync<WorkData>();
            WorkData work = new()
            {
                Id = id,
                ComposerId = composerId,
                Title = title,
                Subtitle = subtitle,
                Genre = genre
            };
            await InsertAsync(work);
        }

        public async Task<IEnumerable<WorkData>> GetFromComposer(ComposerData composer)
        {
            return await GetWhereEqualAsync<WorkData>(nameof(WorkData.ComposerId), composer.Id);
        }
    }
}