namespace HackMe.Application.Models
{
    public class MissionComment
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public string AgentCodeName { get; set; } = null!;
        public string Comment { get; set; } = null!;

        public Agent Agent { get; set; } = null!;
        public Mission Mission { get; set; } = null!;
    }
}
