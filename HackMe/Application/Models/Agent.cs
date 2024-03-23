namespace HackMe.Application.Models
{
    public class Agent
    {
        public string CodeName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Ranking { get; set; } = null!;
        public bool IsDecommissioned { get; set; }
        public string? PersonalData { get; set; }
        public string? ActiveMission { get; set; }
        public int TotalSuccessfulMissions { get; set; }
    }
}
