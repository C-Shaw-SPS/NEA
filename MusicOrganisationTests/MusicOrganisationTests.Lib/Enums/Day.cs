﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Lib.Enums
{
    public enum Day
    {
        Monday = 0b0000001,
        Tuesday = 0b0000010,
        Wednesday = 0b0000100,
        Thursday = 0b0001000,
        Friday = 0b0010000,
        Saturday = 0b0100000,
        Sunday = 0b1000000
    }
}