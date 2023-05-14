using Automarket.DAL;
using DigitalPortfolio.Dal.Interfaces;
using DigitalPortfolio.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPortfolio.Dal.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _db;
        private int id;

        public ProjectRepository(ApplicationDbContext db)
        {
            _db = db;
            id = _db.Projects.Count();
        }

        public async Task<bool> Add(Project entity)
        {
            await _db.Projects.AddAsync(entity);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch ( Exception e)
            {
                var a = e;
            }
            id++;
            return true;
        }

        public async Task<bool> Delete(Project entity)
        {
            _db.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Project> Get()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Project> GetAll()
        {
            return _db.Projects;
        }

        public int Id() => id;

        public async Task<Project> Update(Project entity)
        {
            _db.Projects.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        void IProjectRepository.Add1(Project entity)
        {
             _db.Projects.Add(entity);
            try
            {
                 _db.SaveChanges();
            }
            catch (Exception e)
            {
                var a = e;
            }
            id++;
        }
    }
}
