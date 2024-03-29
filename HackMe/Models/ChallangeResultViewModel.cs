namespace HackMe.Models
{
    public class ChallangeResultViewModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; } = null!;
        public string TaskDescription { get; set; } = null!;
        public string? ExpectedResult { get; set; }
        public bool HasExpectedResult => ExpectedResult != null;

        public DateTime? CompletedOn { get; set; }
        public bool IsCompleted => CompletedOn.HasValue;
    }
}
