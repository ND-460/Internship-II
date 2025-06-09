using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Mission.Entities.Entities;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;
using Mission.Repositories.Repositories;
using Mission.Services.IServices;

namespace Mission.Services.Services
{
    public class MissionService(IMissionRepository missionRepository,IMissionSkillRepository missionSkillRepository) : IMissionService
    {
        public List<Missions> GetMissionList()
        {
            return missionRepository.GetMissionList();
        }

        public Task<string> AddMission(AddMissionRequestModel model)
        {
            return missionRepository.AddMission(model);
        }
        public Task<List<MissionRequestViewModel>> GetAllMissionAsync()
        {
            return missionRepository.GetAllMissionAsync();
        }

        public Task<MissionRequestViewModel?> GetMissionById(int id)
        {
            return missionRepository.GetMissionById(id);
        }

        // int userId
        public async Task<IList<MissionDetailResponseModel>> ClientSideMissionList()
        {
           
            var missions = await missionRepository.ClientSideMissionList();

            return missions.Select(m => new MissionDetailResponseModel()
            {
                Id = m.Id,
                EndDate = m.EndDate,
                StartDate = m.StartDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionTitle = m.MissionTitle,
                TotalSheets = m.TotalSheets,
                RegistrationDeadLine = m.RegistrationDeadLine,
                CityId = m.CityId,
                CityName = m.City.CityName,
                CountryId = m.CountryId,
                CountryName = m.Country.CountryName,
                MissionSkillId = m.MissionSkillId,
                MissionSkillName = missionSkillRepository.GetMissionSkills(m.MissionSkillId),
                MissionThemeId = m.MissionThemeId,
                MissionThemeName = m.MissionTheme.ThemeName
            }).ToList();

        }
    }
}
