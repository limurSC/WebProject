using System.ComponentModel.DataAnnotations;

namespace work.Models
{
    public class Registration
    {
        [Display(Name = "Имя")]
        [Required(ErrorMessage ="Введите имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Введите Фамилию")]
        public string Surname { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Введите почту")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля")]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [MinLength(8, ErrorMessage ="Пароль не менее 8 символов")]
        public string PasswordConfirmation { get; set; }
    }
}
