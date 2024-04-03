using HackMe.Application.Models;

namespace HackMe.Models
{
    public class MissionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsClassified { get; set; }
        public string UrlKey { get; set; } = null!;
        public string? Description { get; set; }

        public string ShortDescription
            => Description != null && Description.Length > 100 ? Description[..100] : string.Empty;

        public IReadOnlyCollection<MissionComment> Comments { get; set; }
            = Array.Empty<MissionComment>();
    }
}
