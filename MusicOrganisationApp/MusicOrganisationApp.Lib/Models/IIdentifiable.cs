﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.Models
{
    public interface IIdentifiable
    {
        public int Id { get; set; }
    }
}