using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mission.Entities.Models;

namespace Mission.Services.IServices
{
    public interface IMissionSkillService
    {
        Task<List<MissionSkillViewModel>> GetAllMissionSkill();

        Task<MissionSkillViewModel?> GetMissionSkillById(int missionSkillId);

        Task<bool> AddMissionSkill(MissionSkillViewModel model);

        Task<bool> UpdateMissionSkill(MissionSkillViewModel model);

        Task<bool> DeleteMissionSkill(int missionSkillId);
    }
}
