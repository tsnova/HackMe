namespace HackMe.Application.Models
{
    public class ChallengeResult
    {
        public int Id { get; set; }
        public string AgentCodeName { get; set; } = null!;
        public int ChallangeTaskId { get; set; }

        public DateTime CompletedOn { get; set; }

        public Agent Agent { get; set; } = null!;
        public ChallengeTask ChallengeTask { get; set; } = null!;
    }

}
