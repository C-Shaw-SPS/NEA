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
            await pupilService.ClearTableAsync<Pupil>();

            Pupil expectedPupil = Expected.Pupils[0];
            await pupilService.InsertPupilAsync(
                expectedPupil.Name,
                expectedPupil.Level,
                expectedPupil.LessonDays,
                expectedPupil.HasDifferentTimes,
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
            await pupilService.ClearTableAsync<Pupil>();
            await pupilService.ClearTableAsync<Caregiver>();
            await pupilService.ClearTableAsync<CaregiverMap>();

            Pupil expectedPupil = Expected.Pupils[0];
            Caregiver expectedCaregiver = Expected.Caregivers[0];

            await pupilService.InsertAsync(expectedPupil);
            await pupilService.InsertAsync(expectedCaregiver);
            await pupilService.InsertExistingCaregiverAsync(expectedPupil.Id, expectedCaregiver.Id, nameof(Caregiver));
            IEnumerable<Caregiver> actualCaregivers = await pupilService.GetCaregiversAsync(expectedPupil.Id);
            Assert.Single(actualCaregivers);
            Assert.Contains(expectedCaregiver, actualCaregivers);
        }
    }
}
