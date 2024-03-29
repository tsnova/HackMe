using HackMe.Application.Models;
using HackMe.Application.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace HackMe.Infrastructure.Data
{
    public interface IRepository
    {
        Task<bool> AgentExists(string codeName);
        Task<Agent?> GetAgent(string codeName, string password);
        Task<Agent?> GetAgent(string codeName);
        string? ValidateAgentLogin(string codeName, string password);
        bool UpdateAgentMission(string codeName, string mission);

        Task<int> CountMissions();
        IList<Mission> GetMissionList(string? searchKey, bool includeClassified);
        Mission? GetMission(int id);
        IReadOnlyCollection<MissionComment> GetMissionComments(string codeName, int missionId);
        void CreateMissionComment(string codeName, int missionId, string comment);

        Task<ChallengeTask?> GetChallenge(int id);
        Task<bool> CreateChallengeResult(string agentCodeName, int taskId);
        Task<IList<ChallangeResultDetailsDto>> GetAllChallenges(string agentCodeName);
    }

    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> AgentExists(string codeName)
        {
            return _dbContext.Agents.AnyAsync(x => x.CodeName == codeName);
        }

        public Task<Agent?> GetAgent(string codeName, string password)
        {
            return _dbContext.Agents.SingleOrDefaultAsync(x => x.CodeName == codeName && x.Password == password);
        }

        public Task<Agent?> GetAgent(string codeName)
        {
            return _dbContext.Agents.SingleOrDefaultAsync(x => x.CodeName == codeName);
        }

        public string? ValidateAgentLogin(string codeName, string password)
        {
            var query = $"SELECT COUNT(*) FROM Agent WHERE CodeName = '{codeName}' AND Password = '{password}'";

            try
            {
                var result = _dbContext.Database.ExecuteSqlRaw(query);
                return result.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during authentication: {ex.Message}");
                return null;
            }
        }

        public Task<int> CountMissions()
        {
            return _dbContext.Missions.CountAsync();
        }

        public IList<Mission> GetMissionList(string? searchKey, bool includeClassified)
        {
            var classified = !includeClassified ? " AND IsClassified = 0" : string.Empty;

            var query = @$"
SELECT [Id]
    ,[Name]
    ,[UrlKey]
    ,[IsActive]
    ,[IsClassified]
    ,[Description]
FROM [dbo].[Mission]
WHERE [Description] LIKE '%{searchKey}%' AND IsActive = 1 {classified} ORDER BY Name";

            try
            {
                var result = _dbContext.Missions.FromSqlRaw(query).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during querying missions: {ex.Message}");
                return new List<Mission>();
            }
        }

        public Mission? GetMission(int id)
        {
            return _dbContext.Missions.SingleOrDefault(x => x.Id == id && x.IsActive);
        }

        public async Task<bool> CreateChallengeResult(string agentCodeName, int taskId)
        {
            var isCompleted = await _dbContext.ChallengeResults
                            .AnyAsync(x => x.AgentCodeName == agentCodeName && x.ChallangeTaskId == taskId);

            if (isCompleted) return false;

            var result = new ChallengeResult
            {
                AgentCodeName = agentCodeName,
                ChallangeTaskId = taskId,
                CompletedOn = DateTime.Now,
            };

            _dbContext.ChallengeResults.Add(result);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IList<ChallangeResultDetailsDto>> GetAllChallenges(string agentCodeName)
        {
            var list = new List<ChallangeResultDetailsDto>();
            var tasks = await _dbContext.ChallengeTasks
                            .OrderBy(x => x.SortOrder).ToListAsync();
            var results = await _dbContext.ChallengeResults
                            .Where(x => x.AgentCodeName == agentCodeName).ToListAsync();

            foreach (var task in tasks)
            {
                var result = results.SingleOrDefault(x => x.ChallangeTaskId == task.Id);

                var item = new ChallangeResultDetailsDto
                {
                    TaskId = task.Id,
                    TaskName = task.Name,
                    TaskDescription = task.Description,
                    ExpectedResult = task.ExpectedResult,
                    DifficultyLevel = task.DifficultyLevel,
                    Score = task.Score,
                    CompletedOn = result?.CompletedOn,
                };
                list.Add(item);
            }

            return list;
        }

        public bool UpdateAgentMission(string codeName, string mission)
        {
            var agent = _dbContext.Agents.SingleOrDefault(x => x.CodeName == codeName);
            if (agent == null)
            {
                return false;
            }

            agent.ActiveMission = mission;
            _dbContext.SaveChanges();
            return true;
        }

        public Task<ChallengeTask?> GetChallenge(int id)
        {
            return _dbContext.ChallengeTasks.SingleOrDefaultAsync(x => x.Id == id);
        }

        public IReadOnlyCollection<MissionComment> GetMissionComments(string codeName, int missionId)
        {
            return _dbContext.MissionComments
                .Where(x => x.AgentCodeName == codeName && x.MissionId == missionId).ToList();
        }

        public void CreateMissionComment(string codeName, int missionId, string comment)
        {
            var missionComment = new MissionComment
            {
                AgentCodeName = codeName,
                MissionId = missionId,
                Comment = comment
            };

            _dbContext.MissionComments.Add(missionComment);
            _dbContext.SaveChanges();
        }
    }
}
