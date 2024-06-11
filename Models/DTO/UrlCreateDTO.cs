using System.ComponentModel.DataAnnotations;

namespace SubUrl.Models.DTO
{
    public class UrlCreateDTO
    {
        [Display(Name = "Ссылка")]
        [Required(ErrorMessage = "Введите ссылку!")]
        [RegularExpression(@"^(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?$", ErrorMessage = "Ссылка неправильная!")]
        public string LongValue { get; set; } = null!;
    }
}