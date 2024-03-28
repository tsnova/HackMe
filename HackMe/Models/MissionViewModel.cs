namespace HackMe.Models
{
    public class MissionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsClassified { get; set; }
        public string? Description { get; set; }

        public string ShortDescription
            => Description != null ? Description.Substring(0, 100) : string.Empty; 
    }
}
