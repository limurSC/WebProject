using System.ComponentModel.DataAnnotations;

namespace DigitalPortfolio.Domain.ViewModel.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите Фамилию")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Введите Почту")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите Пароль")]
        [MinLength(8,ErrorMessage ="Пароль дожен быть не менее 8 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Пароли не сопвадают")]
        public string PasswordConfirmation { get; set; }
    }
}
