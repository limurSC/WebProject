using DigitalPortfolio.Domain.Entity;
using DigitalPortfolio.Domain.Response;
using DigitalPortfolio.Domain.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPortfolio.Service.Interfaces
{
	public interface IAccountService
    {
        BaseResponse<User> Save(User user);
        public Task<BaseResponse<User>> GetByEmail(string email);
        public Task<BaseResponse<User>> GetById(int id);
        public Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
		public Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    }
}
