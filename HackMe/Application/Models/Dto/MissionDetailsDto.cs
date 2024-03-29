namespace HackMe.Application.Models.Dto
{
    public class MissionDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string UrlKey { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsClassified { get; set; }
        public string? Description { get; set; }

        public IReadOnlyCollection<MissionComment> Comments { get; set; } 
            = Array.Empty<MissionComment>();
    }
}
