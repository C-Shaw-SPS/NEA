using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Models
{
    public class Lesson : ILesson<LessonData>, IPupilIdentifiable
    {
        private int _id;
        private int _pupilId;
        private string _pupilName = string.Empty;
        private DateTime _date;
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        private string _notes = string.Empty;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int PupilId
        {
            get => _pupilId;
            set => _pupilId = value;
        }

        public string PupilName
        {
            get => _pupilName;
            set => _pupilName = value;
        }

        public DateTime Date
        {
            get => _date;
            set => _date = value.Date;
        }

        public TimeSpan StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        public TimeSpan EndTime
        {
            get => _endTime;
            set => _endTime = value;
        }

        public string Notes
        {
            get => _notes;
            set => _notes = value;
        }
    }
}