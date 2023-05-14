using DigitalPortfolio.Domain.Entity;
using DigitalPortfolio.Domain.ViewModel;
using DigitalPortfolio.Service.Implementations;
using DigitalPortfolio.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPortfolio.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IAccountService _accountServise;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectController(IProjectService projectService, 
            IWebHostEnvironment webHostEnvironment, IAccountService accountServise)
        {
            _accountServise = accountServise;
            _projectService = projectService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int projectId)
        {
            var project = _projectService.GetById(projectId).Data;
            var response = await _accountServise.GetById(project.AuthorId);
            var author = response.Data;
            author.Project = project;
            return View(author);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Cookies")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Cookies")]
        public async Task<IActionResult> Add(ProjectViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountServise.GetByEmail(User.Identity.Name);
                vm.AuthorId = response.Data.Id;
                var avatar = SaveImage(vm.Image);
                var images = SaveImages(vm.Images, vm.Name);
                _projectService.Add(vm, avatar, images);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        private string SaveImage(IFormFile file)
        {
            string fileName = null;
            if (file != null)
            {
                var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "projectAvatars");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                var filePath = Path.Combine(uploadDir, fileName);
                using (var fileStram = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStram);
                }
            }
            return fileName;
        }

        private string SaveImages(List<IFormFile> files, string ProjectName)
        {
            var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, ProjectName);
            Directory.CreateDirectory(uploadDir);
            foreach(var file in files)
            { 
                var fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                var filePath = Path.Combine(uploadDir, fileName);
                using (var fileStram = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStram);
                }
            }
            return ProjectName;
        }
    }
}
