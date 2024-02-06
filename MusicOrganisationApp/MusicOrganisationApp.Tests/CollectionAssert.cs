namespace MusicOrganisationApp.Tests
{
    public static class CollectionAssert
    {
        public static void Equal<T>(IEnumerable<T> expectedItems, IEnumerable<T> actualItems) where T : IEquatable<T>
        {
            Assert.Equal(expectedItems.Count(), actualItems.Count());
            foreach (T expectedItem in expectedItems)
            {
                Assert.Contains(expectedItem, actualItems);
            }
        }
    }
}