using HackMe.Application.Models;
using HackMe.Infrastructure.Data;

namespace HackMe.Application.Services
{
    public interface IAgentService
    {
        Task<Agent?> GetAgent(string codeName);

        Task<int> CountNews();
        IList<News> GetNewsList(bool includeClassified);
        News? GetNewsItem(int id);
    }

    public class AgentService : IAgentService
    {
        private readonly IRepository _repository;

        public AgentService(IRepository repository)
        {
            _repository = repository;
        }

        public Task<int> CountNews()
        {
            return _repository.CountNews();
        }

        public Task<Agent?> GetAgent(string codeName)
        {
            return _repository.GetAgent(codeName);
        }

        public News? GetNewsItem(int id)
        {
            return _repository.GetNewsItem(id);
        }

        public IList<News> GetNewsList(bool includeClassified)
        {
            return _repository.GetNewsList(includeClassified);
        }
    }
}
