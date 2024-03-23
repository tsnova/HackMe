namespace HackMe.Application.Models
{
    public class ChallengeTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DifficultyLevel { get; set; }
        public int Score { get; set; }
        public int SortOrder { get; set; }
        public string? ExpectedResult { get; set; }

        public ICollection<ChallengeResult> Results { get; set; }
            = new List<ChallengeResult>();
    }

}
