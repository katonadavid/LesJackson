using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class CommandReadDTO
    {
        [Required]
        [MaxLength(250)]
        public int Id { get; set; }
        [Required]
        public string HowTo { get; set; }
        [Required]
        public string Line { get; set; }
        // public string Platform { get; set; }
    }
}