using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Command
    {
        public int Id { get; set; }
        [Required] // To make it non-nullable in DB
        [MaxLength(250)]
        public string HowTo { get; set; }
        [Required]
        public string Line { get; set; }
        [Required]
        public string Platform { get; set; }
    }
}