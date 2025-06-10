using Mission.Entities.Models;
using Mission.Entities;

namespace Mission.Services.IServices
{
    public interface IMissionService
    {
        Task<List<MissionRequestViewModel>> GetAllMissionAsync();
        Task<MissionRequestViewModel?> GetMissionById(int id);
        Task<bool> AddMission(MissionRequestViewModel model);
        Task<IList<MissionDetailResponseModel>> ClientSideMissionList(int userId);

        Task<string> UpdateMission(UpdateMissionRequestModel model);
        Task<string> DeleteMission(int id);

        Task<bool> ApplyMission(AddMissionApplicationRequestModel model);
        List<MissionApplication> GetMissionApplicationList();
        Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication);
        Task<bool> MissionApplicationDelete(UpdateMissionApplicationModel missionApplication);
    }
}
