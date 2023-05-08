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
		public Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

		public Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    }
}
