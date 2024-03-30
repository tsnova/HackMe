using HackMe.Application.Helpers;
using HackMe.Application.Models;
using HackMe.Application.Models.Dto;
using HackMe.Infrastructure.Data;

namespace HackMe.Application.Services
{
    public interface IAgentService
    {
        Task<Agent?> GetAgent(string codeName);
        Task<bool> UpdateAgentActiveMission(string codeName, string mission);

        Task<int> CountMissions();
        IList<Mission> GetMissionsList(string? seachKey, bool includeClassified);
        MissionDetailsDto? GetMissionDetails(string codeName, string urlKey);
        Mission? CreateMissionComment(string codeName, int missionId, string comment);
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

        public IList<Mission> GetMissionsList(string? searchKey, bool includeClassified)
        {
            return _repository.GetMissionList(searchKey, includeClassified);
        }

        public MissionDetailsDto? GetMissionDetails(string codeName, string urlKey)
        {
            var mission = _repository.GetMission(urlKey);
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
                Comments = _repository.GetMissionComments(codeName, mission.Id),
                
            };

            return dto;
        }

        public async Task<bool> UpdateAgentActiveMission(string codeName, string mission)
        {
            if (await _repository.AgentExists(codeName)
                && InputHelper.IsAllowedSqlInjection(mission))
            {
                return _repository.UpdateAgentMission(codeName, mission);
            }

            return false;
        }

        public Mission? CreateMissionComment(string codeName, int missionId, string comment)
        {
            _repository.CreateMissionComment(codeName, missionId, comment);

            return _repository.GetMission(missionId);
        }
    }
}
