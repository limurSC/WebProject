using DigitalPortfolio.Dal.Interfaces;
using DigitalPortfolio.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortfolio.Dal.Repositories
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly ApplicationDbContext _db;


        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<bool> Add(User entity)
        {
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(User entity)
        {
			_db.Remove(entity);
			await _db.SaveChangesAsync();
			return true;
		}

        public async Task<User> Get(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByNameAsync(string name)
		{
			return await _db.Users.FirstOrDefaultAsync(u => u.Name == name);
		}

        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }

		public async Task<User> Update(User entity)
		{
            _db.Users.Update(entity);
			await _db.SaveChangesAsync();

            return entity;
		}
	}
}
