using HackMe.Application.Helpers;
using HackMe.Application.Models;
using HackMe.Application.Models.Dto;
using HackMe.Infrastructure.Data;

namespace HackMe.Application.Services
{
    public interface IAgentService
    {
        Task<Agent?> GetAgent(string codeName);
        Task<bool> UpdateActiveMission(string codeName, string mission);

        Task<int> CountMissions();
        IList<Mission> GetMissionsList(string? seachKey, bool includeClassified);
        MissionDetailsDto? GetMissionDetails(string codeName, int missionId);
        void CreateMissionComment(string codeName, int missionId, string comment);
    }

    public class AgentService : IAgentService
    {
        private readonly IRepository _repository;

        public AgentService(IRepository repository)
        {
            _repository = repository;
        }

        public Task<int> CountMissions()
        {
            return _repository.CountMissions();
        }

        public Task<Agent?> GetAgent(string codeName)
        {
            return _repository.GetAgent(codeName);
        }

        public Mission? GetMission(int id)
        {
            return _repository.GetMission(id);
        }

        public IList<Mission> GetMissionsList(string? searchKey, bool includeClassified)
        {
            return _repository.GetMissionList(searchKey, includeClassified);
        }

        public MissionDetailsDto? GetMissionDetails(string codeName, int missionId)
        {
            var mission = _repository.GetMission(missionId);
            if (mission == null)
            {
                return null;
            }

            var dto = new MissionDetailsDto
            {
                Id = mission.Id,
                IsActive = mission.IsActive,
                Description = mission.Description,
                IsClassified = mission.IsClassified,
                Name = mission.Name,
                UrlKey = mission.UrlKey,
                Comments = _repository.GetMissionComments(codeName, missionId),
                
            };

            return dto;
        }

        public async Task<bool> UpdateActiveMission(string codeName, string mission)
        {
            if (await _repository.AgentExists(codeName)
                && InputHelper.IsAllowedSqlInjection(mission))
            {
                return _repository.UpdateAgentMission(codeName, mission);
            }

            return false;
        }

        public void CreateMissionComment(string codeName, int missionId, string comment)
        {
            _repository.CreateMissionComment(codeName, missionId, comment);
        }
    }
}
