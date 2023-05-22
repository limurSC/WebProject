using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DigitalPortfolio.Dal.Interfaces;
using work.Models;
using DigitalPortfolio.Service.Interfaces;

namespace work.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectService _projectService;

        public HomeController(IProjectService projectRepository)
        {
            _projectService = projectRepository;
        }

        public IActionResult Index()
        {
            if (User.Identity.Name != null)
            {
                //var a = _projectService.GetByEmail(User.Identity.Name).Data;
                var a = _projectService.GetAll().Data;
                return View(a);
            }
            return View();
        }

        #region hz
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}