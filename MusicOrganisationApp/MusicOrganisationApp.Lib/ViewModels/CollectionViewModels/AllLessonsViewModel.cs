﻿using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AllLessonsViewModel : CollectionViewModelBase<LessonData>
    {
        public const string ROUTE = nameof(AllLessonsViewModel);

        private readonly LessonService _service;

        [ObservableProperty]
        private DateTime _selectedDate = DateTime.Today;

        public AllLessonsViewModel() : base(LessonViewModel.ROUTE, EditLessonSlotViewModel.ROUTE)
        {
            _service = new(_database);
        }

        protected override IService<LessonData> Service => _service;

        async partial void OnSelectedDateChanged(DateTime value)
        {
            _service.Date = value;
            await RefreshAsync();
        }
    }
}