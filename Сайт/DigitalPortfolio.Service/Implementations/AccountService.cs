using DigitalPortfolio.Dal.Interfaces;
using DigitalPortfolio.Dal.Repositories;
using DigitalPortfolio.Domain.Entity;
using DigitalPortfolio.Domain.Enum;
using DigitalPortfolio.Domain.Helpers;
using DigitalPortfolio.Domain.Response;
using DigitalPortfolio.Domain.ViewModel.User;
using DigitalPortfolio.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Security.Claims;

namespace DigitalPortfolio.Service.Implementations
{
	public class AccountService : IAccountService
	{
		private readonly IUserRepository<User> _userRepository;
		private readonly ILogger<AccountService> _logger;

        public AccountService(IUserRepository<User> userRepository, 
            ILogger<AccountService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<User>> GetByEmail(string email)
        {
            try
            {
                var users = _userRepository.GetAll();
                var a=    users.First(u => u.Email.Equals(email));
                return new BaseResponse<User> { Data = a, StatusCode = Domain.Enum.StatusCode.OK};
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"[GetByEmail]: {e.Message}");
                return new BaseResponse<User>
                {
                    Description = e.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<User>> GetById(int id)
        {
            try
            {
                var users = _userRepository.GetAll();
                var a = users.First(u => u.Id == id);
                return new BaseResponse<User> { Data = a, StatusCode = Domain.Enum.StatusCode.OK };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"[GetById]: {e.Message}");
                return new BaseResponse<User>
                {
                    Description = e.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetByEmail(model.Email);
                if (user == null)
                    return new BaseResponse<ClaimsIdentity> { Description = "Пользователь не зарегестрирован" };

                var modelPassword = HashPasswordHelper.HashPassword(model.Password);
                if (user.Password != modelPassword)
                    return new BaseResponse<ClaimsIdentity> { Description = "Неверный пароль" };

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>
                {
                    Data = result,
                    StatusCode = Domain.Enum.StatusCode.OK
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"[Login]: {e.Message}");
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = e.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user != null)
                    return new BaseResponse<ClaimsIdentity> 
                    { 
                        Description = "Пользователь с данной почтой уже зарегестрирован" 
                    };

                user = new User
                {
                    Id = _userRepository.Id() + 1,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    Password = HashPasswordHelper.HashPassword(model.Password)
                };

                await _userRepository.Add(user);
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>
                {
                    Data = result,
                    Description = "Пользователь зарегестрирован",
                    StatusCode = Domain.Enum.StatusCode.OK
                };
            }
            catch   (Exception e) 
            {
                _logger.LogError(e, $"[Register]: {e.Message}");
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = e.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<User> Save(User user)
        {
            try
            {
                var a = _userRepository.Get();
                var userToChange = a.FirstOrDefault(u => u.Id == user.Id);

                userToChange.Profession = user.Profession;
                userToChange.City = user.City;
                userToChange.Country= user.Country;
                userToChange.Description = user.Description;
                userToChange.Image=user.Image;
                
                _userRepository.Update(userToChange);

                return new BaseResponse<User>()
                {
                    Data = userToChange,
                    Description = "Данные обновлены",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"[Save]: {e.Message}");
                return new BaseResponse<User>
                {
                    Description = e.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                //new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims,"ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
