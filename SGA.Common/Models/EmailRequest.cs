using System.ComponentModel.DataAnnotations;

namespace SGA.Common.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
