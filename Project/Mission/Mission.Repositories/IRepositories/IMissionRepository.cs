using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mission.Entities.Entities;
using Mission.Entities.Models;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionRepository
    {
        List<Missions> GetMissionList();
        Task<string> AddMission(AddMissionRequestModel model);
    }
}
