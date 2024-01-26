using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditWorkViewModel : EditViewModelBase<Work>, IQueryAttributable
    {
        private const string _EDIT_PAGE_TITLE = "Edit work";
        private const string _NEW_PAGE_TITLE = "New work";

        private readonly WorkService _service;

        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private string _subtitle = string.Empty;

        [ObservableProperty]
        private string _genre = string.Empty;

        [ObservableProperty]
        private string _composerName = string.Empty;

        public EditWorkViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _service = new(_database);
        }

        protected override IService<Work> Service => _service;

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
