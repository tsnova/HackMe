namespace HackMe.Application.Models.Dto
{
    public class TeamChallengeDto
    {
        public string AgentCodeName { get; set; } = null!;
        public int TotalScore => Results.Sum(x => x.Score);

        public ICollection<ChallangeResultDetailsDto> Results { get; set; }
            = new List<ChallangeResultDetailsDto>();
    }
}
