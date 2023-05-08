using DigitalPortfolio.Dal.Interfaces;
using DigitalPortfolio.Domain.Entity;
using DigitalPortfolio.Domain.ViewModel.User;
using DigitalPortfolio.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace DigitalPortfolio.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsers();
            if(response.StatusCode == Domain.Enum.StatusCode.Ok)
                return View(response.Data);

            return Redirect("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            var response = await _userService.GetUser(id);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                return View(response.Data);

            return Redirect("Error");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _userService.Delete(id);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                return Redirect("/");

			return Redirect("Error");
		}

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if(id == 0)
                return View();

            var response = await _userService.GetUser(id);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                return View(response.Data);

			return Redirect("Error");
		}

        [HttpPost]
        public async Task<IActionResult> Save(UserViewModel model)
        {
            if (ModelState.IsValid)
                if (model.Id == 0)
                {
                    byte[] imageData;
                    using(var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);

                    await _userService.Add(model);
                }
                else
                    await _userService.Edit(model);

            return Redirect("Error");
        }
    }
}
