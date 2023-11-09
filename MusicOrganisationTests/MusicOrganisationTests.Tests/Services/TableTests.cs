﻿using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Services;

namespace MusicOrganisationTests.Tests.Services
{
    public class TableTests
    {
        [Fact]
        public async Task TestCaregiverSql()
        {
            await TestType(Expected.Caregivers);
        }

        [Fact]
        public async Task TestCaregiverMapSql()
        {
            await TestType(Expected.CaregiverMaps);
        }

        [Fact]
        public async Task TestComposerSql()
        {
            await TestType(Expected.Composers);
        }

        [Fact]
        public async Task TestFixedLessonSql()
        {
            await TestType(Expected.FixedLessons);
        }

        [Fact]
        public async Task TestLessonSql()
        {
            await TestType(Expected.Lessons);
        }

        [Fact]
        public async Task TestLessonRestrictionSql()
        {
            await TestType(Expected.LessonRestrictions);
        }

        [Fact]
        public async Task TestLessonTimeSql()
        {
            await TestType(Expected.LessonTimes);
        }

        [Fact]
        public async Task TestWorkSql()
        {
            await TestType(Expected.Works);
        }

        [Fact]
        public async Task TestRepertoireSql()
        {
            await TestType(Expected.Repertoires);
        }

        [Fact]
        public async Task TestPupilSql()
        {
            await TestType(Expected.Pupils);
        }

        private async static Task TestType<T>(IEnumerable<T> expectedItems) where T : class, ITable, new()
        {
            Service table = new(nameof(TableTests));
            await table.ClearTableAsync<T>();
            await table.InsertAllAsync(expectedItems);
            IEnumerable<T> actual = await table.GetAllAsync<T>();
            Assert.Equal(expectedItems.Count(), actual.Count());
            foreach (T expectedItem in expectedItems)
            {
                Assert.Contains(expectedItem, actual);
            }
        }
    }
}