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

            PupilData expectedPupil = Expected.Pupils[0];
            await pupilService.InsertPupilAsync(
                expectedPupil.Name,
                expectedPupil.Level,
                expectedPupil.LessonDays,
                expectedPupil.HasDifferentTimes,
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

            PupilData expectedPupil = Expected.Pupils[0];
            CaregiverData expectedCaregiver = Expected.Caregivers[0];

            await pupilService.InsertAsync(expectedPupil);
            await pupilService.InsertAsync(expectedCaregiver);
            await pupilService.InsertExistingCaregiverAsync(expectedPupil.Id, expectedCaregiver.Id, nameof(CaregiverData));
            IEnumerable<CaregiverData> actualCaregivers = await pupilService.GetCaregiversAsync(expectedPupil.Id);
            Assert.Single(actualCaregivers);
            Assert.Contains(expectedCaregiver, actualCaregivers);
        }
    }
}
