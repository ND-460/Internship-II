using Microsoft.EntityFrameworkCore;
using Mission.Entities;
using Mission.Entities.Context;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;

namespace Mission.Repositories.Repositories
{
    public class MissionRepository(MissionDbContext dbContext, IMissionThemeRepository missionThemeRepository) : IMissionRepository
    {
        private readonly MissionDbContext _dbContext = dbContext;
        private readonly IMissionThemeRepository _missionThemeRepository = missionThemeRepository;

        public Task<List<MissionRequestViewModel>> GetAllMissionAsync()
        {
            return (from m in _dbContext.Missions
                    join t in _dbContext.MissionThemes on m.MissionThemeId equals t.Id
                    where !m.IsDeleted
                    select new MissionRequestViewModel
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
                        MissionThemeName = t.ThemeName
                    }).ToListAsync();
        }


        public async Task<MissionRequestViewModel?> GetMissionById(int id)
        {
            return await _dbContext.Missions.Where(m => m.Id == id).Select(m => new MissionRequestViewModel()
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

        public async Task<bool> AddMission(Missions model)
        {
            try
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


                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                };
                await dbContext.Missions.AddAsync(missions);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        // int userId
        public async Task<IList<Missions>> ClientSideMissionList()
        {
            return await _dbContext.Missions
                .Include(m => m.City)
                .Include(m => m.Country)
               .Include(m => m.MissionTheme)
               .Include(m => m.MissionApplications)
                .Where(m => !m.IsDeleted)
                .OrderBy(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task<bool> ApplyMission(AddMissionApplicationRequestModel model)
        {
            try
            {
                var mission = _dbContext.Missions.Where(x => x.Id == model.MissionId).FirstOrDefault();

                if (mission == null) throw new Exception("Mission not found");

                var application = _dbContext.MissionApplications.Where(x => x.MissionId == model.MissionId && x.UserId == model.UserId).FirstOrDefault();

                if (application != null) throw new Exception("Already applied!");

                MissionApplication app = new MissionApplication()
                {
                    UserId = model.UserId,
                    MissionId = model.MissionId,
                    AppliedDate = model.AppliedDate,
                    Seats = model.Sheet,
                    Status = model.Status,

                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                };

                mission.TotalSheets -= model.Sheet;

                await _dbContext.MissionApplications.AddAsync(app);
                _dbContext.Missions.Update(mission);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<GetMissionApplicationList> GetMissionApplicationList()
        {
            var result = (from ma in _dbContext.MissionApplications
                          join user in _dbContext.User on ma.UserId equals user.Id
                          join mission in _dbContext.Missions on ma.MissionId equals mission.Id
                          join theme in _dbContext.MissionThemes on mission.MissionThemeId equals theme.Id
                          where !ma.IsDeleted
                          select new GetMissionApplicationList
                          {
                              Id = ma.Id,
                              MissionId = ma.MissionId,
                              UserId = ma.UserId,
                              AppliedDate = ma.AppliedDate,
                              Status = ma.Status,
                              Seats = ma.Seats,
                              UserName = user.FirstName + " " + user.LastName,
                              MissionTitle = mission.MissionTitle,
                              MissionTheme = theme.ThemeName
                          }).ToList();

            return result;
        }


        public async Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication)
        {
            var tMissionApp = _dbContext.MissionApplications.Where(x => x.Id == missionApplication.Id).FirstOrDefault();

            if (tMissionApp == null) throw new Exception("Mission application not found");

            tMissionApp.Status = true;
            //tMissionApp.IsDeleted = true;
            tMissionApp.ModifiedDate = DateTime.Now;

            _dbContext.MissionApplications.Update(tMissionApp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> MissionApplicationDelete(UpdateMissionApplicationModel missionApplication)
        {
            var tMissionApp = _dbContext.MissionApplications.Where(x => x.Id == missionApplication.Id).FirstOrDefault();

            if (tMissionApp == null) throw new Exception("Mission application not found");

            tMissionApp.Status = false;
            tMissionApp.IsDeleted = true;
            tMissionApp.ModifiedDate = DateTime.Now;

            _dbContext.MissionApplications.Update(tMissionApp);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateMission(UpdateMissionRequestModel model)
        {
            var existingMission = await dbContext.Missions
                .Where(x => x.Id == model.Id && !x.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingMission == null)
                throw new Exception("Mission not found!");

            var isExist = await dbContext.Missions
                .AnyAsync(x => x.MissionTitle == model.MissionTitle &&
                               x.StartDate == model.StartDate &&
                               x.EndDate == model.EndDate &&
                               x.CityId == model.CityId &&
                               !x.IsDeleted &&
                               x.Id != model.Id);

            if (isExist)
                throw new Exception("Another mission with the same details already exists!");


            dbContext.Entry(existingMission).CurrentValues.SetValues(model);

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<string> DeleteMission(int id)
        {
            var mission = await dbContext.Missions
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (mission == null)
                throw new Exception("Mission not found!");

            mission.IsDeleted = true;
            mission.ModifiedDate = DateTime.Now;

            await dbContext.SaveChangesAsync();

            return "Mission deleted successfully!";
        }
    }
}
