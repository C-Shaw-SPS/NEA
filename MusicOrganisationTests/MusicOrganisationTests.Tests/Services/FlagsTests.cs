﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicOrganisationTests.Lib.Services;

namespace MusicOrganisationTests.Tests.Services
{
    public class FlagsTests
    {
        private const uint FLAGS = 0b01101011;

        [Theory]
        [InlineData(FLAGS, 0, true)]
        [InlineData(FLAGS, 1, true)]
        [InlineData(FLAGS, 2, false)]
        [InlineData(FLAGS, 3, true)]
        [InlineData(FLAGS, 4, false)]
        [InlineData(FLAGS, 5, true)]
        [InlineData(FLAGS, 6, true)]
        [InlineData(FLAGS, 7, false)]
        public void TestHasFlag(uint flags, int index, bool expectedHasFlag)
        {
            bool actualHasFlag = flags.HasFlagAtIndex(index);
            Assert.Equal(expectedHasFlag, actualHasFlag);
        }

        [Fact]
        public void TestAddFlag()
        {
            for (int i = 0; i < 8; ++i)
            {
                uint flags = FLAGS;
                flags.AddFlagAtIndex(i);
                Assert.True(flags.HasFlagAtIndex(i));
            }
        }

        [Fact]
        public void TestRemoveFlag()
        {
            for (int i = 0; i < 8; ++i)
            {
                uint flags = FLAGS;
                flags.RemoveFlagAtIndex(i);
                Assert.False(flags.HasFlagAtIndex(i));
            }
        }
    }
}