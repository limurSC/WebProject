using DigitalPortfolio.Dal.Interfaces;
using DigitalPortfolio.Dal.Repositories;
using DigitalPortfolio.Domain.Entity;
using DigitalPortfolio.Domain.Helpers;
using DigitalPortfolio.Domain.Response;
using DigitalPortfolio.Domain.ViewModel;
using DigitalPortfolio.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPortfolio.Service.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository<User> _userRepository;
        private readonly ILogger<AccountService> _logger;

        public ProjectService(IUserRepository<User> userRepository, 
            IProjectRepository projectRepository, ILogger<AccountService> logger)
        {
            _logger = logger;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public BaseResponse<Project> Add(ProjectViewModel vm, string avatar, string images)
        {
            try
            {
                var result = new Project
                {
                    Description = vm.Description,
                    AuthorId = vm.AuthorId,
                    Id = _projectRepository.Id() + 1,
                    Image = avatar,
                    Images = images,
                    Name = vm.Name,
                    GraphicalDesign = vm.GraphicalDesign,
                    ProductDesign = vm.ProductDesign,
                    InteractiveDesign = vm.InteractiveDesign,
                    ClothDesign = vm.ClothDesign,
                    WebDesign = vm.WebDesign,
                    Photo = vm.Photo,
                    Art = vm.Art,
                    Illustration = vm.Illustration,
                    AdPhoto = vm.AdPhoto,
                    DigitalArt = vm.DigitalArt,
                };

                _projectRepository.Add1(result);

                return new BaseResponse<Project>
                {
                    Data = result,
                    StatusCode = Domain.Enum.StatusCode.OK
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"[Add]: {e.Message}");
                return new BaseResponse<Project>
                {
                    Description = e.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<List<Project>> GetByEmail(string email)
        {
            try
            {
                var author = _userRepository.GetAll().FirstOrDefault(u => u.Email == email);
                var result = _projectRepository.GetAll().Where(p => p.AuthorId == author.Id).ToList();
                return new BaseResponse<List<Project>> { Data = result };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"[GetByEmail]: {e.Message}");
                return new BaseResponse<List<Project>>
                {
                    Description = e.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<Project> GetById(int id)
        {
            try
            {
                var project = _projectRepository.GetAll().FirstOrDefault(p => p.Id == id);
                return new BaseResponse<Project> { Data = project };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"[GetById]: {e.Message}");
                return new BaseResponse<Project>
                {
                    Description = e.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}
