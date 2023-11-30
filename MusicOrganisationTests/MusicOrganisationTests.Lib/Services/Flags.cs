using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Lib.Services
{
    public static class Flags
    {
        public static bool HasFlagAtIndex(this int flags, int index)
        {
            return ((flags >> index) & 1) == 1;
        }

        public static void AddFlagAtIndex(this ref int flags, int index)
        {
            int newFlags = 1 << index;
            flags |= newFlags;
        }

        public static void RemoveFlagAtIndex(this ref int flags, int index)
        {
            int remainingFlags = ~(1 << index);
            flags &= remainingFlags;
        }
    }
}