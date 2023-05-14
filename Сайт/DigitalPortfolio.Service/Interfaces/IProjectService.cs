using DigitalPortfolio.Domain.Entity;
using DigitalPortfolio.Domain.Response;
using DigitalPortfolio.Domain.ViewModel;

namespace DigitalPortfolio.Service.Interfaces
{
    public interface IProjectService
    {
        public BaseResponse<Project> Add(ProjectViewModel vm, string avatar, string images);
        public BaseResponse<List<Project>> GetByEmail(string email);
        public BaseResponse<Project> GetById(int id);
    }
}
