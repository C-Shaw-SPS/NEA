using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditLessonViewModel : EditLessonViewModelBase<LessonData>
    {
        public const string ROUTE = nameof(EditLessonViewModel);

        private const string _EDIT_PAGE_TITLE = "Edit lesson";
        private const string _NEW_PAGE_TITLE = "New lesson";

        private readonly LessonService _service;

        [ObservableProperty]
        private DateTime _date;

        [ObservableProperty]
        private string _notes = string.Empty;

        public EditLessonViewModel() : base(LessonViewModel.ROUTE, _EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _service = new(_database);
        }

        protected override LessonServiceBase<LessonData> LessonService => _service;

        protected override object SelectedDateObject => Date;

        protected override void SetNonTimeValuesToSave()
        {
            _value.Date = Date;
            _value.Notes = Notes;
        }

        protected override void SetDisplayNonTimeValues()
        {
            Date = _value.Date;
            Notes = _value.Notes;
        }
    }
}