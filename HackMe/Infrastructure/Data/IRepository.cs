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

        Task<int> CountNews();
        IList<News> GetNewsList(bool includeClassified);
        News? GetNewsItem(int id);

        Task CreateChallengeResult(string agentCodeName, int taskId);
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
            var query = $"SELECT CodeName FROM Agent WHERE CodeName = '{codeName}' AND Password = '{password}'";

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

        public Task<int> CountNews()
        {
            return _dbContext.News.CountAsync();
        }

        public IList<News> GetNewsList(bool includeClassified)
        {
            var query = _dbContext.News
                            .Where(x => x.IsActive);

            if (!includeClassified)
                query = query.Where(x => !x.IsClassified);

            return query
                    .OrderBy(x => x.Name)
                    .ToList();
        }

        public News? GetNewsItem(int id)
        {
            return _dbContext.News.SingleOrDefault(x => x.Id == id && x.IsActive);
        }

        public async Task CreateChallengeResult(string agentCodeName, int taskId)
        {
            var item = await _dbContext.ChallengeResults
                            .SingleOrDefaultAsync(x => x.AgentCodeName == agentCodeName && x.ChallangeTaskId == taskId);

            if (item != null) return;

            item = new ChallengeResult
            {
                AgentCodeName = agentCodeName,
                ChallangeTaskId = taskId,
                CompletedOn = DateTime.Now,
            };

            _dbContext.ChallengeResults.Add(item);
            await _dbContext.SaveChangesAsync();
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
                    DifficultyLevel = task.DifficultyLevel,
                    Score = task.Score,
                    CompletedOn = result?.CompletedOn,
                };
                list.Add(item);
            }

            return list;
        }
    }
}
