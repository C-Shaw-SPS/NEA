using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Services
{
    internal static class ExpectedService
    {
        public static readonly List<ComposerData> ComposerData = new()
        {
            new ComposerData
            {
                Id = 0,
                Name = "Bach"
            },
            new ComposerData
            {
                Id = 1,
                Name = "Mozart"
            }
        };

        public static readonly List<WorkData> WorkData = new()
        {
            new WorkData
            {
                Id = 0,
                ComposerId = ComposerData[0].Id,
                Title = "Violin Concerto in A minor"
            },
            new WorkData
            {
                Id = 1,
                ComposerId = ComposerData[0].Id,
                Title = "Toccata & Fugue in D minor"
            },
            new WorkData
            {
                Id = 2,
                ComposerId = ComposerData[1].Id,
                Title = "Violin Concerto in G major"
            },
            new WorkData
            {
                Id = 3,
                ComposerId = ComposerData[1].Id,
                Title = "Eine Kleine Nachtmusik"
            }
        };

        public static readonly List<RepertoireData> RepertoireData = new()
        {
            new RepertoireData
            {
                Id = 0,
                WorkId = WorkData[0].Id
            },
            new RepertoireData
            {
                Id = 1,
                WorkId = WorkData[1].Id
            },
            new RepertoireData
            {
                Id = 2,
                WorkId = WorkData[2].Id
            },
            new RepertoireData
            {
                Id = 3,
                WorkId = WorkData[3].Id
            }
        };

        public static readonly List<Work> Works = GetWorks();

        private static List<Work> GetWorks()
        {
            List<Work> works = new();
            for (int i = 0; i < WorkData.Count; ++i)
            {
                Work work = new()
                {
                    WorkId = WorkData[i].Id,
                    ComposerId = WorkData[i].ComposerId,
                    Title = WorkData[i].Title,
                    ComposerName = ComposerData[WorkData[i].ComposerId].Name
                };
                works.Add(work);
            }
            return works;
        }
    }
}