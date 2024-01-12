using MusicOrganisation.Lib.Enums;
using MusicOrganisation.Lib.Models;
using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;

namespace MusicOrganisation.Tests.Services
{
    public class PupilServiceTests
    {
        [Fact]
        public async Task TestAddPupilAsync()
        {
            PupilService pupilService = new(nameof(TestAddPupilAsync));
            await pupilService.DropTableIfExistsAsync<Pupil>();


            Pupil expectedPupil = Expected.Pupils[0];
            await pupilService.InsertPupilAsync(
                expectedPupil.Name,
                expectedPupil.Level,
                expectedPupil.NeedsDifferentTimes,
                expectedPupil.LessonDuration,
                expectedPupil.MondayLessonSlots,
                expectedPupil.TuesdayLessonSlots,
                expectedPupil.WednesdayLessonSlots,
                expectedPupil.ThursdayLessonSlots,
                expectedPupil.FridayLessonSlots,
                expectedPupil.SaturdayLessonSlots,
                expectedPupil.SundayLessonSlots,
                expectedPupil.Email,
                expectedPupil.PhoneNumber
                );
            IEnumerable<Pupil> actualPupils = await pupilService.GetAllAsync<Pupil>();
            Assert.Single(actualPupils);
            Assert.Contains(expectedPupil, actualPupils);
        }

        [Fact]
        public async Task TestAddExistingCaregiverAsync()
        {
            PupilService pupilService = new(nameof(TestAddExistingCaregiverAsync));
            await pupilService.DropTableIfExistsAsync<Pupil>();
            await pupilService.DropTableIfExistsAsync<CaregiverData>();
            await pupilService.DropTableIfExistsAsync<CaregiverMap>();

            Pupil expectedPupil = Expected.Pupils[0];
            CaregiverData expectedCaregiverData = Expected.CaregiverData[0];

            await pupilService.InsertAsync(expectedPupil);
            await pupilService.InsertAsync(expectedCaregiverData);
            await pupilService.InsertExistingCaregiverAsync(expectedPupil.Id, expectedCaregiverData.Id, Expected.CaregiverMap[0].Description);
            IEnumerable<Caregiver> actualCaregivers = await pupilService.GetCaregiversAsync(expectedPupil.Id);
            Assert.Single(actualCaregivers);
            Assert.Contains(Expected.Caregiver, actualCaregivers);
        }

        [Fact]
        public async Task TestGetRepertoireAsync()
        {
            PupilService pupilService = new(nameof(TestGetRepertoireAsync));
            await Task.WhenAll
            (
                pupilService.DropTableIfExistsAsync<Pupil>(),
                pupilService.DropTableIfExistsAsync<RepertoireData>(),
                pupilService.DropTableIfExistsAsync<WorkData>(),
                pupilService.DropTableIfExistsAsync<ComposerData>()
            );

            Pupil pupil = new()
            {
                Id = 0
            };

            List<ComposerData> composerData = new()
            {
                new ComposerData
                {
                    Id = 0,
                    Name = "Composer 0"
                },
                new ComposerData
                {
                    Id = 1,
                    Name = "Composer 1"
                }
            };

            List<WorkData> workData = new()
            {
                new WorkData
                {
                    Id = 0,
                    Title = "Work 0",
                    Subtitle = "Subtitle 0",
                    Genre = "Genre 0",
                    ComposerId = composerData[0].Id
                },
                new WorkData
                {
                    Id = 1,
                    Title = "Work 1",
                    Subtitle = "Subtitle 1",
                    Genre = "Genre 1",
                    ComposerId = composerData[1].Id
                }
            };

            List<RepertoireData> repertoireData = new()
            {
                new RepertoireData
                {
                    Id = 0,
                    PupilId = pupil.Id,
                    WorkId = workData[0].Id,
                    DateStarted = new DateTime(2023 ,11, 24),
                    Syllabus = "Syllabus 0",
                    Status = RepertoireStatus.CurrentlyLearning
                },
                new RepertoireData
                {
                    Id = 1,
                    PupilId = pupil.Id,
                    WorkId = workData[1].Id,
                    DateStarted = new DateTime(2022, 11, 24),
                    Syllabus = "Syllabus 1",
                    Status = RepertoireStatus.FinishedLearning
                }
            };

            List<Repertoire> expectedRepertoires = new()
            {
                new Repertoire
                {
                    RepertoireId = repertoireData[0].Id,
                    DateStarted = repertoireData[0].DateStarted,
                    Syllabus = repertoireData[0].Syllabus,
                    Status = repertoireData[0].Status,
                    WorkId = workData[0].Id,
                    Title = workData[0].Title,
                    Subtitle = workData[0].Subtitle,
                    ComposerId = composerData[0].Id,
                    Genre = workData[0].Genre,
                    ComposerName = composerData[0].Name
                },
                new Repertoire
                {
                    RepertoireId = repertoireData[1].Id,
                    DateStarted = repertoireData[1].DateStarted,
                    Syllabus = repertoireData[1].Syllabus,
                    Status = repertoireData[1].Status,
                    WorkId = workData[1].Id,
                    Title = workData[1].Title,
                    Subtitle = workData[1].Subtitle,
                    ComposerId = composerData[1].Id,
                    Genre = workData[1].Genre,
                    ComposerName = composerData[1].Name
                }
            };

            await Task.WhenAll
            (
                pupilService.InsertAsync(pupil),
                pupilService.InsertAllAsync(composerData),
                pupilService.InsertAllAsync(workData),
                pupilService.InsertAllAsync(repertoireData)
            );

            IEnumerable<Repertoire> actualRepertoires = await pupilService.GetRepertoireAsync(pupil.Id);
            Assert.Equal(expectedRepertoires.Count, actualRepertoires.Count());
            foreach (Repertoire expectedRepertoire in expectedRepertoires)
            {
                Assert.Contains(expectedRepertoire, actualRepertoires);
            }
        }
    }
}