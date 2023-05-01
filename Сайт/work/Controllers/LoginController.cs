using Microsoft.AspNetCore.Mvc;

namespace work.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
