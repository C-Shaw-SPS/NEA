﻿using MusicOrganisationTests.Lib.Json;
using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Tables;
using MusicOrganisationTests.Lib.Services;
using System.Reflection.Metadata;
using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.App.TimetableTestCases;

namespace MusicOrganisationTests.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            await TimetableTestCase1.CreateTestCase();
        }

        static async Task CreateEmptyDatabase()
        {
            await Task.WhenAll(
                CreateAndInitTable<CaregiverData>(),
                CreateAndInitTable<CaregiverMap>(),
                CreateAndInitTable(ComposerGetter.GetFromOpenOpus()),
                CreateAndInitTable<LessonData>(),
                CreateAndInitTable<LessonSlotData>(),
                CreateAndInitTable<Pupil>(),
                CreateAndInitTable<RepertoireData>(),
                CreateAndInitTable(WorkGetter.GetFromOpenOpus())
            );
        }

        static async Task CreateAndInitTable<T>(IEnumerable<T>? data = null) where T : class, ITable, new()
        {
            Service service = new(DatabaseProperties.NAME);
            await service.ClearTableAsync<T>();
            if (data is not null)
            {
                await service.InsertAllAsync(data);
            }
        }
    }
}