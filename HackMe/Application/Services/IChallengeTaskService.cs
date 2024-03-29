using HackMe.Application.Models.Dto;
using HackMe.Infrastructure.Data;

namespace HackMe.Application.Services
{
    public interface IChallengeTaskService
    {
        Task<TeamChallengeDto> GetAll(string agentCodeName);

        Task<bool> CreateResult(string agentCodeName, int taskId, string? input);
    }

    public class ChallengeTaskService : IChallengeTaskService
    {
        private readonly IRepository _repository;

        public ChallengeTaskService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<TeamChallengeDto> GetAll(string agentCodeName)
        {
            var dto = new TeamChallengeDto { AgentCodeName = agentCodeName };
            if (!await _repository.AgentExists(agentCodeName))
            {
                return dto;
            }

            var tasks = await _repository.GetAllChallenges(agentCodeName);
            dto.Results = tasks;

            return dto;
        }

        public async Task<bool> CreateResult(string agentCodeName, int taskId, string? input)
        {
            if (await _repository.AgentExists(agentCodeName))
            {
                var task = await _repository.GetChallenge(taskId);
                if (task == null)
                {
                    return false;
                }

                if (task.ExpectedResult != null && input != null && input != task.ExpectedResult)
                {
                    return false;
                }

                return await _repository.CreateChallengeResult(agentCodeName, taskId);
            }

            return false;
        }
    }
}
