using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public class EditPupilViewModel : EditViewModelBase<Pupil>, IQueryAttributable
    {
        private const string _EDIT_PAGE_TITLE = "Edit pupil";
        private const string _NEW_PAGE_TITLE = "New pupil";

        private readonly PupilService _service;

        public EditPupilViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _service = new(_database);
        }

        protected override IService<Pupil> Service => _service;

        protected override void SetDisplayValues()
        {
            throw new NotImplementedException();
        }

        protected override bool TrySetValuesToSave()
        {
            throw new NotImplementedException();
        }
    }
}