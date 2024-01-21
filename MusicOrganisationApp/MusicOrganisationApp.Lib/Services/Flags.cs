using MusicOrganisationApp.Lib.Exceptions;

namespace MusicOrganisationApp.Lib.Services
{
    public static class Flags
    {
        public const int MAX_FLAG_INDEX = 32;

        public static bool HasFlagAtIndex(this int flags, int index)
        {
            return (flags >> index & 1) == 1;
        }

        /// <summary>
        /// Adds a flag at the specified index
        /// </summary>
        /// <returns>
        /// True if a new flag was added at the index, Flase if there was already a flag at the index
        /// </returns>
        public static bool AddFlagAtIndex(this ref int flags, int index)
        {
            bool hadFlagAtIndex = flags.HasFlagAtIndex(index);
            int newFlags = 1 << index;
            flags |= newFlags;
            return hadFlagAtIndex;
        }

        /// <summary>
        /// Removes a flag at the specified index
        /// </summary>
        /// <returns>
        /// True if an existing flag was removed at the index, False if there was no flag at the index
        /// </returns>
        public static bool RemoveFlagAtIndex(this ref int flags, int index)
        {
            bool hadFlagAtIndex = flags.HasFlagAtIndex(index);
            int remainingFlags = ~(1 << index);
            flags &= remainingFlags;
            return hadFlagAtIndex;
        }

        public static int GetNumberOfFlags(this int flags)
        {
            int count = 0;
            while (flags != 0)
            {
                if ((flags & 1) == 1)
                {
                    count++;
                }
                flags >>= 1;
            }
            return count;
        }

        public static int GetIndexOfFirstFlag(this int flags)
        {
            for (int i = 0; i < MAX_FLAG_INDEX; ++i)
            {
                if (flags.HasFlagAtIndex(i))
                {
                    return i;
                }
            }

            throw new FlagsException("No flags set to true");
        }

        public static int GetNewFlags(params int[] indexes)
        {
            int flags = 0;
            foreach (int index in indexes)
            {
                flags.AddFlagAtIndex(index);
            }
            return flags;
        }
    }
}