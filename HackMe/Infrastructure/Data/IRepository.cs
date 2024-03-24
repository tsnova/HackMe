using HackMe.Application.Models;
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
        IList<News> GetNewsList(bool? classified);
        News? GetNewsItem(int id);
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

        public IList<News> GetNewsList(bool? classified)
        {
            var query = _dbContext.News.OrderBy(x => x.Name);

            if (classified == null)
                return query.ToList();

            return _dbContext.News
                    .Where(x => x.IsClassified == classified)
                    .ToList();
        }

        public News? GetNewsItem(int id)
        {
            return _dbContext.News.Find(id);
        }
    }
}
