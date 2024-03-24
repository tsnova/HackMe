using System.ComponentModel.DataAnnotations;

namespace HackMe.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(20)]
        public string? CodeName { get; set; }

        [Required]
        [StringLength(100)]
        public string? Password { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
