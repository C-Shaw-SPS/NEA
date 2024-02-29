using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Tests.ViewModels
{
    public class EditPupilViewModelTests
    {
        private const string _VALID_LESSON_HOURS = "01";
        private const string _INVALID_LESSON_HOURS = "01a";

        private const string _VALID_EMAIL_ADDRESS = "pupil@email.com";
        private const string _INVALID_EMAIL_ADDRESS = "pupilAtEmail.com";

        private const string _VALID_PHONE_NUMBER = "+44 0123 456789";
        private const string _INVALID_PHONE_NUMBER = "123abc";

        [Fact]
        public void TestLessonDurationInputValidation()
        {
            EditPupilViewModel viewModel = new(nameof(TestLessonDurationInputValidation), true)
            {
                LessonHours = _VALID_LESSON_HOURS
            };
            Assert.Equal(_VALID_LESSON_HOURS, viewModel.LessonHours);
            viewModel.LessonHours = _INVALID_LESSON_HOURS;
            Assert.Equal(_VALID_LESSON_HOURS, viewModel.LessonHours);
        }

        [Fact]
        public async Task TestEmptyLessonDurationAsync()
        {
            EditPupilViewModel viewModel = new(nameof(TestEmptyLessonDurationAsync), true);
            await viewModel.TrySaveCommand.ExecuteAsync(null);
            Assert.True(viewModel.IsCurrentViewModel);
            Assert.NotEmpty(viewModel.LessonDurationError);
        }

        [Theory]
        [InlineData(_VALID_EMAIL_ADDRESS, true)]
        [InlineData(_INVALID_EMAIL_ADDRESS, false)]
        public async Task TestEmailAddressValidation(string emailAddress, bool expectedIsValid)
        {
            EditPupilViewModel viewModel = new(nameof(TestEmailAddressValidation), true)
            {
                EmailAddress = emailAddress
            };
            await viewModel.TrySaveCommand.ExecuteAsync(null);
            bool actualIsValid = viewModel.EmailAddressError == string.Empty;
            Assert.Equal(expectedIsValid, actualIsValid);
        }

        [Theory]
        [InlineData(_VALID_PHONE_NUMBER, true)]
        [InlineData(_INVALID_PHONE_NUMBER, false)]
        public async Task TestPhoneNumberValidation(string phoneNumber, bool expectedIsValid)
        {
            EditPupilViewModel viewModel = new(nameof(TestEmailAddressValidation), true)
            {
                PhoneNumber = phoneNumber
            };
            await viewModel.TrySaveCommand.ExecuteAsync(null);
            bool actualIsValid = viewModel.PhoneNumberError == string.Empty;
            Assert.Equal(expectedIsValid, actualIsValid);
        }
    }
}