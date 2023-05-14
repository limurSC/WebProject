using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DigitalPortfolio.Domain.ViewModel.User
{
	public class UserViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public string Surname { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
			
		public string Password { get; set; }
    }
}
