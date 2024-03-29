using HackMe.Application.Enums;

namespace HackMe.Models
{
    public class ChallangeResultsViewModel
    {
        public string? ValidationMessage { get; set; }

        public IEnumerable<ChallangeResultViewModel> Results { get; set; }
            = Enumerable.Empty<ChallangeResultViewModel>();

        public bool HasCompletedTask(ChallengeTaskType type)
        {
            return this.Results.Any(x => x.TaskId == (int)type && x.IsCompleted);
        }
    }
}
