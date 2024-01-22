using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Services
{
    internal static class ExpectedComposerService
    {
        public static readonly List<ComposerData> Composers = new()
        {
            new ComposerData
            {
                Id = 0
            },
            new ComposerData
            {
                Id = 1
            }
        };

        public static readonly List<WorkData> Works = new()
        {
            new WorkData
            {
                Id = 0,
                ComposerId = Composers[0].Id
            },
            new WorkData
            {
                Id = 1,
                ComposerId = Composers[0].Id
            },
            new WorkData
            {
                Id = 2,
                ComposerId = Composers[1].Id
            },
            new WorkData
            {
                Id = 3,
                ComposerId = Composers[1].Id
            }
        };

        public static readonly List<RepertoireData> Repertoires = new()
        {
            new RepertoireData
            {
                Id = 0,
                WorkId = Works[0].Id
            },
            new RepertoireData
            {
                Id = 1,
                WorkId = Works[1].Id
            },
            new RepertoireData
            {
                Id = 2,
                WorkId = Works[2].Id
            },
            new RepertoireData
            {
                Id = 3,
                WorkId = Works[3].Id
            }
        };
    }
}