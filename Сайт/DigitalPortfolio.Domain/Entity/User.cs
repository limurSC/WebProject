

using DigitalPortfolio.Domain.Enum;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPortfolio.Domain.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите Фамилию")]
        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Description { get; set; }
        public string? Profession { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? Secret { get; set; }
        [NotMapped]
        public Project? Project { get; set; }
    }
}
