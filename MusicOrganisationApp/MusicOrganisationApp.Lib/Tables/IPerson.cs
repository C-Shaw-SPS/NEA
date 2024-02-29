using MusicOrganisationApp.Lib.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.Tables
{
    public interface IPerson : IIdentifiable
    {
        public string Name { get; set; }
    }
}