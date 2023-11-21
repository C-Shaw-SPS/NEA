using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Services;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.Tests.Services
{
    public class PupilServiceTests
    {
        [Fact]
        public async Task TestAddPupilAsync()
        {
            PupilService pupilService = new(nameof(TestAddPupilAsync));
            await pupilService.ClearTableAsync<PupilData>();


            PupilData expectedPupil = Expected.PupilData[0];
            await pupilService.InsertPupilAsync(
                expectedPupil.Name,
                expectedPupil.Level,
                expectedPupil.HasDifferentTimes,
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
            IEnumerable<PupilData> actualPupils = await pupilService.GetAllAsync<PupilData>();
            Assert.Single(actualPupils);
            Assert.Contains(expectedPupil, actualPupils);
        }

        [Fact]
        public async Task TestAddExistingCaregiverAsync()
        {
            PupilService pupilService = new(nameof(TestAddExistingCaregiverAsync));
            await pupilService.ClearTableAsync<PupilData>();
            await pupilService.ClearTableAsync<CaregiverData>();
            await pupilService.ClearTableAsync<CaregiverMap>();

            PupilData expectedPupil = Expected.PupilData[0];
            CaregiverData expectedCaregiverData = Expected.CaregiverData[0];

            await pupilService.InsertAsync(expectedPupil);
            await pupilService.InsertAsync(expectedCaregiverData);
            await pupilService.InsertExistingCaregiverAsync(expectedPupil.Id, expectedCaregiverData.Id, Expected.CaregiverMap[0].Description);
            IEnumerable<Caregiver> actualCaregivers = await pupilService.GetCaregiversAsync(expectedPupil.Id);
            Assert.Single(actualCaregivers);
            Assert.Contains(Expected.Caregiver, actualCaregivers);
        }
    }
}