﻿using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Tests.Models
{
    public class SqlTests
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

        private async static Task TestType<T>(IEnumerable<T> expectedItems) where T : class, ISqlStorable, new()
        {
            TableConnection<T> table = new(nameof(SqlTests));
            await table.ClearAsync();
            await table.InsertAllAsync(expectedItems);
            IEnumerable<T> actual = await table.GetAllAsync();
            Assert.Equal(expectedItems.Count(), actual.Count());
            foreach (T expectedItem in expectedItems)
            {
                Assert.Contains(expectedItem, actual);
            }
        }
    }
}