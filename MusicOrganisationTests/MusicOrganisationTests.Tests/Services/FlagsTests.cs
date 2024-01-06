using MusicOrganisationTests.Lib.Services;

namespace MusicOrganisationTests.Tests.Services
{
    public class FlagsTests
    {
        private const int FLAGS = 0b01101011;

        [Theory]
        [InlineData(FLAGS, 0, true)]
        [InlineData(FLAGS, 1, true)]
        [InlineData(FLAGS, 2, false)]
        [InlineData(FLAGS, 3, true)]
        [InlineData(FLAGS, 4, false)]
        [InlineData(FLAGS, 5, true)]
        [InlineData(FLAGS, 6, true)]
        [InlineData(FLAGS, 7, false)]
        public void TestHasFlag(int flags, int index, bool expectedHasFlag)
        {
            bool actualHasFlag = flags.HasFlagAtIndex(index);
            Assert.Equal(expectedHasFlag, actualHasFlag);
        }

        [Fact]
        public void TestAddFlag()
        {
            for (int i = 0; i < Flags.MAX_FLAG_INDEX; ++i)
            {
                int flags = FLAGS;
                flags.AddFlagAtIndex(i);
                Assert.True(flags.HasFlagAtIndex(i));
            }
        }

        [Fact]
        public void TestRemoveFlag()
        {
            for (int i = 0; i < Flags.MAX_FLAG_INDEX; ++i)
            {
                int flags = FLAGS;
                flags.RemoveFlagAtIndex(i);
                Assert.False(flags.HasFlagAtIndex(i));
            }
        }

        [Theory]
        [InlineData(0b1111, 4)]
        [InlineData(0b0, 0)]
        [InlineData(0b1010, 2)]
        [InlineData(0b0110, 2)]
        public void TestGetNumberOfFlags(int flags, int expectedCount)
        {
            int actualCount = flags.GetNumberOfFlags();
            Assert.Equal(expectedCount, actualCount);
        }
    }
}