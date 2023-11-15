using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Lib.Services
{
    public static class Flags
    {
        public static bool HasFlagAtIndex(this uint flags, int index)
        {
            return ((flags >> index) & 1) == 1;
        }

        public static void AddFlagAtIndex(this ref uint flags, int index)
        {
            uint newFlags = 1u << index;
            flags |= newFlags;
        }

        public static void RemoveFlagAtIndex(this ref uint flags, int index)
        {
            uint remainingFlags = ~(1u << index);
            flags &= remainingFlags;
        }
    }
}