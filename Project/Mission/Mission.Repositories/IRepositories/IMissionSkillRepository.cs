using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mission.Entities.Entities;
using Mission.Entities.Models;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionSkillRepository
    {
         Task<List<MissionSkillViewModel>> GetAllMissionSkill();

        Task<MissionSkillViewModel?> GetMissionSkillById(int missionThemeId);

        Task<bool> AddMissionSkill(MissionSkill missionSkill);

        Task<bool> UpdateMissionSkill(MissionSkill missionSkill);

        Task<bool> DeleteMissionSkill(int missionSkillId);
    }
}
