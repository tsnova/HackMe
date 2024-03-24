namespace HackMe.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsClassified { get; set; }
        public string? Description { get; set; }
    }
}
