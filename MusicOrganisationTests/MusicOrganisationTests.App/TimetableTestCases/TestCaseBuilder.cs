using MusicOrganisationTests.Lib.Json;
using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Services;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.App.TimetableTestCases
{
    public class TestCaseBuilder
    {
        private readonly List<LessonSlotData> _lessonSlots;
        private readonly List<Pupil> _pupils;
        private readonly List<LessonData> _previousLessons;
        private readonly string _databaseName;
        private readonly static IEnumerable<ComposerData> _composerData = ComposerGetter.GetFromOpenOpus();
        private readonly static IEnumerable<WorkData> _workData = WorkGetter.GetFromOpenOpus();

        public TestCaseBuilder(List<LessonSlotData> lessonSlots, List<Pupil> pupils, List<LessonData> previousLessons, string databaseName)
        {
            _lessonSlots = lessonSlots;
            _pupils = pupils;
            _previousLessons = previousLessons;
            _databaseName = databaseName;
        }

        public async Task CreateDatabase()
        {
            if (File.Exists(_databaseName))
            {
                File.Delete(_databaseName);
            }

            Service service = new(_databaseName);
            await Task.WhenAll(
                service.InsertAllAsync(_lessonSlots),
                service.InsertAllAsync(_pupils),
                service.InsertAllAsync(_previousLessons),
                service.InsertAllAsync(_composerData),
                service.InsertAllAsync(_workData)
            );
        }
    }
}