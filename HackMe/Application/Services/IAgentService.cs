using HackMe.Application.Helpers;
using HackMe.Application.Models;
using HackMe.Infrastructure.Data;

namespace HackMe.Application.Services
{
    public interface IAgentService
    {
        Task<Agent?> GetAgent(string codeName);
        Task<bool> UpdateActiveMission(string codeName, string mission);

        Task<int> CountMissions();
        IList<Mission> GetMissionsList(bool includeClassified);
        Mission? GetMission(int id);
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

        public IList<Mission> GetMissionsList(bool includeClassified)
        {
            return _repository.GetMissionList(includeClassified);
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
    }
}
