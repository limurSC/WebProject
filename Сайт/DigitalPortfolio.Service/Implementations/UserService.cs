using DigitalPortfolio.Domain.Entity;
using DigitalPortfolio.Domain.Response;
using DigitalPortfolio.Domain.Enum;
using DigitalPortfolio.Service.Interfaces;
using DigitalPortfolio.Dal.Interfaces;
using DigitalPortfolio.Domain.ViewModel.User;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortfolio.Service.Implementations
{
	public class UserService : IUserService
	{
		private readonly IUserRepository<User> _userRepository;

		public UserService(IUserRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<IBaseResponse<UserViewModel>> Add(UserViewModel userViewModel)
		{
			var baseResponse = new BaseResponse<UserViewModel>();
			try
			{
				var user = new User()
				{
					Email = userViewModel.Email,
					Password = userViewModel.Password,
					Name = userViewModel.Name,
					Surname = userViewModel.Surname,
				};

				await _userRepository.Add(user);
			}
			catch (Exception e)
			{
				return new BaseResponse<UserViewModel>()
				{
					Description = $"[CreateUser] : {e.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}

			return baseResponse;
		}

        public Task<bool> Create(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<bool>> Delete(User user)
		{
			throw new NotImplementedException();
		}

        public Task<IBaseResponse<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<User>> Edit(UserViewModel model)
		{
			var baseResponse = new BaseResponse<User>();
			try 
			{
				var user = await _userRepository.GetAll().FirstOrDefaultAsync(u=>u.Id== model.Id);
				if(user == null) 
				{
					baseResponse.StatusCode = StatusCode.UserNotFound;
					baseResponse.Description = "User not Found";
					return baseResponse;
				}

				user.Surname = model.Surname;
				user.Email = model.Email;
				user.Password = model.Password;
				user.Name = model.Name;

				await _userRepository.Update(user);

				return baseResponse;
			}
			catch (Exception e) 
			{
				return new BaseResponse<User>()
				{
					Description = $"[Edit] : {e.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
		}

        public IQueryable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<User>> GetUser(int id)
		{
			var baseResponse = new BaseResponse<User>();
			try
			{
				var user = await _userRepository.GetAll().FirstOrDefaultAsync(u=>id == u.Id);
				if (user == null)
				{
					baseResponse.Description = "User not Found";
					baseResponse.StatusCode = StatusCode.UserNotFound;
					return baseResponse;
				}

				baseResponse.Data = user;
				return baseResponse;
			}
			catch (Exception e) 
			{
				return new BaseResponse<User>()
				{
					Description = $"[GetUser] : {e.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
		}

		public Task<IBaseResponse<User>> GetUserByName(string name)
		{
			throw new NotImplementedException();
		}

		public IBaseResponse<IEnumerable<User>> GetUsers()
		{
			var baseResponse = new BaseResponse<IEnumerable<User>>();
			try
			{
				var users = _userRepository.GetAll();
				if(users.Count() == 0)
				{
					baseResponse.Description = "Найдено 0 пользователей";
					baseResponse.StatusCode = StatusCode.Ok;
					return baseResponse;
				}

				baseResponse.Data = users;
				baseResponse.StatusCode = StatusCode.Ok;
				return baseResponse;
			}
			catch (Exception e) 
			{
				return new BaseResponse<IEnumerable<User>>()
				{
					Description = $"[GetUsers] : {e.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
		}

        Task<IBaseResponse<IEnumerable<User>>> IUserService.GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
