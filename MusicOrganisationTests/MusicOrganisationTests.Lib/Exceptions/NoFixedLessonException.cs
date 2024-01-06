using MusicOrganisationTests.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Lib.Exceptions
{
    public class NoFixedLessonException : Exception
    {
        public NoFixedLessonException(Pupil pupil) : base($"Pupil Id: {pupil.Id} Name: {pupil.Name} does not have a fixed lesson slot") { }
    }
}