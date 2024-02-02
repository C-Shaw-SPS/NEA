using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Services
{
    internal static class ExpectedService
    {
        public static readonly List<ComposerData> ComposerDatas = new()
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

        public static readonly List<WorkData> WorkDatas = new()
        {
            new WorkData
            {
                Id = 0,
                ComposerId = ComposerDatas[0].Id,
                Title = "Violin Concerto in A minor"
            },
            new WorkData
            {
                Id = 1,
                ComposerId = ComposerDatas[0].Id,
                Title = "Toccata & Fugue in D minor"
            },
            new WorkData
            {
                Id = 2,
                ComposerId = ComposerDatas[1].Id,
                Title = "Violin Concerto in G major"
            },
            new WorkData
            {
                Id = 3,
                ComposerId = ComposerDatas[1].Id,
                Title = "Eine Kleine Nachtmusik"
            }
        };

        public static readonly List<Pupil> Pupils = GetPupils(2);

        public static readonly List<RepertoireData> RepertoireDatas = new()
        {
            new RepertoireData
            {
                Id = 0,
                WorkId = WorkDatas[0].Id,
                PupilId = Pupils[0].Id
            },
            new RepertoireData
            {
                Id = 1,
                WorkId = WorkDatas[1].Id,
                PupilId = Pupils[0].Id
            },
            new RepertoireData
            {
                Id = 2,
                WorkId = WorkDatas[2].Id,
                PupilId = Pupils[1].Id
            },
            new RepertoireData
            {
                Id = 3,
                WorkId = WorkDatas[3].Id,
                PupilId = Pupils[1].Id
            }
        };

        public static readonly List<CaregiverData> CaregiverDatas = GetCaregiverData(4);

        public static readonly List<CaregiverMap> CaregiverMaps = GetCaregiverMaps();

        public static readonly List<LessonData> LessonDatas = new()
        {
            new LessonData
            {
                Id = 0,
                PupilId  = Pupils[0].Id
            },
            new LessonData
            {
                Id = 1,
                PupilId = Pupils[0].Id
            },
            new LessonData
            {
                Id = 2,
                PupilId  = Pupils[1].Id
            },
            new LessonData
            {
                Id = 3,
                PupilId = Pupils[1].Id
            }
        };

        public static readonly List<PupilAvaliability> PupilAvaliabilities = new()
        {
            new PupilAvaliability
            {
                Id = 0,
                PupilId = Pupils[0].Id
            },
            new PupilAvaliability
            {
                Id = 1,
                PupilId = Pupils[0].Id
            },
            new PupilAvaliability
            {
                Id = 2,
                PupilId = Pupils[1].Id
            },
            new PupilAvaliability
            {
                Id = 3,
                PupilId = Pupils[1].Id
            },
        };

        public static readonly List<Work> Works = GetWorks();

        public static readonly List<Repertoire> Repertoires = GetRepertoires();

        public static readonly List<PupilCaregiver> Caregivers = GetCaregivers();

        private static List<Pupil> GetPupils(int count)
        {
            List<Pupil> pupils = [];
            for (int i = 0; i < count; ++i)
            {
                Pupil pupil = new()
                {
                    Id = i,
                    Name = $"Pupil {i}",
                    Level = $"Level {i}",
                    NeedsDifferentTimes = Convert.ToBoolean(i % 2),
                    LessonDuration = new TimeSpan(i, 0, 0),
                    Email = $"Email {i}",
                    PhoneNumber = $"Phone number {i}",
                    Notes = $"Notes {i}"
                };
                pupils.Add(pupil);
            }
            return pupils;
        }

        private static List<CaregiverData> GetCaregiverData(int count)
        {
            List<CaregiverData> caregiverData = [];
            for (int i = 0; i < count; ++i)
            {
                CaregiverData caregiver = new()
                {
                    Id = i,
                    Name = $"Caregiver {i}",
                    PhoneNumber = $"Phone number {i}",
                    Email = $"Email {i}"
                };
                caregiverData.Add(caregiver);
            }
            return caregiverData;
        }

        private static List<CaregiverMap> GetCaregiverMaps()
        {
            List<CaregiverMap> caregiverMaps = [];
            for (int i = 0; i < CaregiverDatas.Count; ++i)
            {
                CaregiverMap caregiverMap = new()
                {
                    Id = i,
                    CaregiverId = CaregiverDatas[i].Id,
                    PupilId = Pupils[i % Pupils.Count].Id
                };
                caregiverMaps.Add(caregiverMap);
            }
            return caregiverMaps;
        }

        private static List<PupilCaregiver> GetCaregivers()
        {
            List<PupilCaregiver> caregivers = [];
            foreach (CaregiverMap caregiverMap in CaregiverMaps)
            {
                CaregiverData caregiverData = CaregiverDatas[caregiverMap.CaregiverId];
                PupilCaregiver caregiver = new()
                {
                    Id = caregiverData.Id,
                    Name = caregiverData.Name,
                    Email = caregiverData.Email,
                    PhoneNumber = caregiverData.PhoneNumber,
                    CaregiverId = caregiverMap.Id,
                    Description = caregiverMap.Description,
                    PupilId = caregiverMap.PupilId
                };
                caregivers.Add(caregiver);
            }
            return caregivers;
        }

        private static List<Work> GetWorks()
        {
            List<Work> works = new();
            for (int i = 0; i < WorkDatas.Count; ++i)
            {
                Work work = new()
                {
                    Id = WorkDatas[i].Id,
                    ComposerId = WorkDatas[i].ComposerId,
                    Title = WorkDatas[i].Title,
                    ComposerName = ComposerDatas[WorkDatas[i].ComposerId].Name
                };
                works.Add(work);
            }
            return works;
        }

        private static List<Repertoire> GetRepertoires()
        {
            List<Repertoire> repertoires = [];
            foreach (RepertoireData repertoireData in RepertoireDatas)
            {
                WorkData workData = WorkDatas[repertoireData.WorkId];
                ComposerData composerData = ComposerDatas[workData.ComposerId];
                Assert.Equal(repertoireData.WorkId, workData.Id);
                Assert.Equal(workData.ComposerId, composerData.Id);
                Repertoire repertoire = new()
                {
                    Id = repertoireData.Id,
                    DateStarted = repertoireData.DateStarted,
                    Syllabus = repertoireData.Syllabus,
                    IsFinishedLearning = repertoireData.IsFinishedLearning,
                    PupilId = repertoireData.PupilId,
                    WorkId = repertoireData.WorkId,
                    Title = workData.Title,
                    Subtitle = workData.Subtitle,
                    ComposerId = workData.ComposerId,
                    Genre = workData.Genre,
                    ComposerName = composerData.Name,
                    Notes = repertoireData.Notes
                };
                repertoires.Add(repertoire);
            }
            return repertoires;
        }
    }
}