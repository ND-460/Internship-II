using Mission.Entities;
using Mission.Entities.Models;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionRepository
    {
        Task<List<MissionRequestViewModel>> GetAllMissionAsync();
        Task<MissionRequestViewModel?> GetMissionById(int id);
        Task<bool> AddMission(Missions mission);
        Task<IList<Missions>> ClientSideMissionList();

        Task<bool> ApplyMission(AddMissionApplicationRequestModel model);

        List<GetMissionApplicationList> GetMissionApplicationList();
        Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication);

        Task<bool> MissionApplicationDelete(UpdateMissionApplicationModel missionApplication);
        Task<bool> UpdateMission(UpdateMissionRequestModel model);

        Task<string> DeleteMission(int id);
    }
}
