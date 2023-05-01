using Microsoft.AspNetCore.Mvc;
using work.Models;

namespace work.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Check(Registration regInfo)
        {
            if(ModelState.IsValid)
                return Redirect("/");

            return View("Index");
        }
    }
}
