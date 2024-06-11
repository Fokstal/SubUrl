using System.ComponentModel.DataAnnotations;

namespace SubUrl.Models.DTO
{
    public class UrlDTO
    {
        public int Id { get; set; }

        [Display(Name = "Ссылка")]
        [Required(ErrorMessage = "Введите ссылку!")]
        [RegularExpression(@"^(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?$", ErrorMessage = "Ссылка неправильная!")]
        public string LongValue { get; set; } = null!;

        [Display(Name = "Сокращение")]
        [Required(ErrorMessage = "Введите сокращение!")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Длина сокращения должна быть 6 символов!")]
        public string ShortValue { get; set; } = null!;
    }
}