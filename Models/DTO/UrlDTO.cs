using System.ComponentModel.DataAnnotations;

namespace SubUrl.Models.DTO
{
    public class UrlDTO
    {
        public int Id { get; set; }
        
        [Required]
        [RegularExpression(@"^(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?$", ErrorMessage = "Url is not correct!")]
        public string LongValue { get; set; } = null!;
    }
}