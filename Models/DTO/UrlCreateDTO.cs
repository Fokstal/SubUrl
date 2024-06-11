using System.ComponentModel.DataAnnotations;

namespace SubUrl.Models.DTO
{
    public class UrlCreateDTO
    {   
        [Required(ErrorMessage = "Link is required!")]
        [RegularExpression(@"^(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?$", ErrorMessage = "Url is not correct!")]
        public string LongValue { get; set; } = null!;
    }
}