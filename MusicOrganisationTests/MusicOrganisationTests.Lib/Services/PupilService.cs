﻿using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Enums;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.Lib.Services
{
    public class PupilService : Service
    {
        public PupilService(string path) : base(path)
        {

        }

        public async Task AddPupil(string name, string level, Day lessonDays, bool hasDifferentTimes, string? email, string? phoneNumber)
        {
            int id = await GetNextIdAsync<Pupil>();
            Pupil pupil = new()
            {
                Id = id,
                Name = name,
                Level = level,
                LessonDays = lessonDays,
                HasDifferentTimes = hasDifferentTimes,
                Email = email,
                PhoneNumber = phoneNumber
            };
            await InsertAsync(pupil);
        }

        public async Task AddNewCaregiver(string name, string? email, string? phoneNumber, int pupilId, string description)
        {
            int caregiverId = await GetNextIdAsync<Caregiver>();

            Caregiver caregiver = new()
            {
                Id = caregiverId,
                Name = name,
                Email = email,
                PhoneNumber = phoneNumber
            };

            await InsertAsync(caregiver);
            await AddExistingCaregiver(pupilId, caregiverId, description);
        }

        public async Task AddExistingCaregiver(int pupilId, int caregiverId, string description)
        {
            int id = await GetNextIdAsync<CaregiverMap>();
            CaregiverMap caregiverMap = new()
            {
                Id = id,
                PupilId = pupilId,
                CaregiverId = caregiverId,
                Description = description
            };

            await InsertAsync(caregiverMap);
        }

        public async Task<IEnumerable<Caregiver>> GetCaregiversAsync(int pupilId)
        {
            await InitAsync<Caregiver>();
            await InitAsync<CaregiverMap>();
            string query = $"""
                SELECT Caregivers.Id, Caregivers.Name, Caregivers.Email, Caregivers.PhoneNumber
                FROM Caregivers
                JOIN CaregiverMap
                ON Caregivers.Id = CaregiverMap.CaregiverId
                WHERE CaregiverMap.PupilId = {pupilId}
                """;

            IEnumerable<Caregiver> result = await _connection.QueryAsync<Caregiver>(query);
            return result;
        }

        public async Task<IEnumerable<Work>> GetRepertoireAsync(int pupilId)
        {
            throw new NotImplementedException();
        }
    }
}