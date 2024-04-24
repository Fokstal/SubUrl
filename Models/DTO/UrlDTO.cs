using System.ComponentModel.DataAnnotations;

namespace SubUrl.Models.DTO
{
    public class UrlDTO
    {
        public int Id { get; set; }
        
        [Required]
        public string LongValue { get; set; } = null!;
    }
}