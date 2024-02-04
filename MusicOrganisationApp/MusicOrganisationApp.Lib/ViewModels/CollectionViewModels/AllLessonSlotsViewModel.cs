﻿using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public class AllLessonSlotsViewModel : CollectionViewModelBase<LessonSlotData>
    {
        public const string ROUTE = nameof(AllLessonSlotsViewModel);

        private static readonly List<DayOfWeek> _daysOfWeek = LessonSlotService.GetDaysOfWeek();

        private readonly LessonSlotService _service;

        public AllLessonSlotsViewModel() : base(LessonSlotViewModel.ROUTE, EditLessonSlotViewModel.ROUTE)
        {
            _service = new(_database);
        }

        public List<DayOfWeek> DaysOfWeek => _daysOfWeek;

        protected override IService<LessonSlotData> Service => _service;
    }
}