using HackMe.Application.Models;

namespace HackMe.Application.Services
{
    public interface IChallengeTaskService
    {
        Task<ChallengeTask> GetAll();

        Task<int> GetResults();
    }
}
