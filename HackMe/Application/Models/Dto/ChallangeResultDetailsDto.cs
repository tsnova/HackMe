namespace HackMe.Application.Models.Dto
{
    public class ChallangeResultDetailsDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; } = null!;
        public string TaskDescription { get; set; } = null!;
        public string? ExpectedResult { get; set; }

        public int DifficultyLevel { get; set; }
        public int Score { get; set; }
        public DateTime? CompletedOn { get; set; }
    }
}
