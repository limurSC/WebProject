using DigitalPortfolio.Domain.Entity;
using DigitalPortfolio.Domain.Response;
using DigitalPortfolio.Domain.ViewModel;

namespace DigitalPortfolio.Service.Interfaces
{
    public interface IProjectService
    {
        public BaseResponse<Project> Add(ProjectViewModel vm, string avatar, string images);
        public BaseResponse<List<Project>> GetByEmail(string email);
        public Task<BaseResponse<List<Project>>> GetBySearchQuery(string graphicalDesign, string productDesign,
            string interactiveDesign, string clothDesign, string webDesign, string photo,
            string art, string illustration, string adPhoto, string digitalArt, string searchText);
        public BaseResponse<Project> GetById(int id);
    }
}
