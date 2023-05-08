using DigitalPortfolio.Dal.Interfaces;
using DigitalPortfolio.Domain.Entity;
using DigitalPortfolio.Domain.Response;
using DigitalPortfolio.Domain.ViewModel.User;

namespace DigitalPortfolio.Service.Interfaces
{
	public interface IUserService
	{
		Task<IBaseResponse<IEnumerable<User>>> GetUsers();

		Task<IBaseResponse<User>> GetUser(int id);

		Task<IBaseResponse<UserViewModel>> Add(UserViewModel userViewModel);

		Task<IBaseResponse<bool>> Delete(int id);

		Task<IBaseResponse<User>> GetUserByName(string name);

		Task<IBaseResponse<User>> Edit(UserViewModel model); 
	}
}
