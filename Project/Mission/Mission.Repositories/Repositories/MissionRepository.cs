using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mission.Entities.Context;
using Mission.Entities.Entities;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;

namespace Mission.Repositories.Repositories
{
    public class MissionRepository(MissionDbContext dbContext) : IMissionRepository
    {
        public List<Missions> GetMissionList()
        {
            return dbContext.Missions.Where(x => !x.IsDeleted).ToList();
        }

        public async Task<string> AddMission(AddMissionRequestModel model)
        {
            var isExist = dbContext.Missions.Where(x =>
                            x.MissionTitle == model.MissionTitle
                            && x.StartDate == model.StartDate
                            && x.EndDate == model.EndDate
                            && x.CityId == model.CityId
                            && !x.IsDeleted
                        ).FirstOrDefault();

            if (isExist != null) throw new Exception("Mission already exist!");

            Missions missions = new Missions()
            {
                MissionTitle = model.MissionTitle,
                MissionDescription = model.MissionDescription,
                MissionImages = model.MissionImages,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CountryId = model.CountryId,
                CityId = model.CityId,
                TotalSheets = model.TotalSheets,
                MissionThemeId = model.MissionThemeId,
                MissionSkillId = model.MissionSkillId,
                //MissionOrganisationName = "",
                //MissionOrganisationDetail = "",
                //MissionType = "",
                //MissionDocuments = "",
                //MissionAvailability = "",
                //MissionVideoUrl = "",


                IsDeleted = false,
                CreatedDate = DateTime.Now,
            };
            await dbContext.Missions.AddAsync(missions);
            dbContext.SaveChanges();

            return "Added!";
        }
        public Task<List<MissionRequestViewModel>> GetAllMissionAsync()
        {
            return dbContext.Missions.Select(m => new MissionRequestViewModel()
            {
                Id = m.Id,
                CityId = m.CityId,
                CountryId = m.CountryId,
                EndDate = m.EndDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionSkillId = m.MissionSkillId,
                MissionThemeId = m.MissionThemeId,
                MissionTitle = m.MissionTitle,
                StartDate = m.StartDate,
                TotalSeats = m.TotalSheets ?? 0,
            }).ToListAsync();
        }

        public async Task<MissionRequestViewModel?> GetMissionById(int id)
        {
            return await dbContext.Missions.Where(m => m.Id == id).Select(m => new MissionRequestViewModel()
            {
                Id = m.Id,
                CityId = m.CityId,
                CountryId = m.CountryId,
                EndDate = m.EndDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionSkillId = m.MissionSkillId,
                MissionThemeId = m.MissionThemeId,
                MissionTitle = m.MissionTitle,
                StartDate = m.StartDate,
                TotalSeats = m.TotalSheets ?? 0,
            }).FirstOrDefaultAsync();
        }
        public async Task<IList<Missions>> ClientSideMissionList()
        {
            return await dbContext.Missions
                .Include(m => m.City)
                .Include(m => m.Country)
               .Include(m => m.MissionTheme)
                .Where(m => !m.IsDeleted)
                .OrderBy(m => m.CreatedDate)
                .ToListAsync();
        }
    }
}
