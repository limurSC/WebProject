using System.ComponentModel.DataAnnotations;

namespace DigitalPortfolio.Domain.ViewModel.User
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Введите Почту")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        

        [Required(ErrorMessage = "Введите Пароль")]
        [MinLength(8, ErrorMessage = "Пароль дожен быть не менее 8 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
