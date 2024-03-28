namespace HackMe.Models
{
    public class ChallangeResultViewModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; } = null!;
        public string TaskDescription { get; set; } = null!;

        public DateTime? CompletedOn { get; set; }
        public bool IsCompleted => CompletedOn.HasValue;
    }
}
