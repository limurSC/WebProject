using Automarket.DAL;
using DigitalPortfolio.Dal.Interfaces;
using DigitalPortfolio.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortfolio.Dal.Repositories
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly ApplicationDbContext _db;

        private int id { set ; get; }

        public int Id() => id;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
            id = db.Users.Count();
        }


        public async Task<bool> Add(User entity)
        {
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
            id++;
            return true;
        }

        public async Task<bool> Delete(User entity)
        {
			_db.Remove(entity);
			await _db.SaveChangesAsync();
			return true;
		}

        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }

		public async Task<User> Update(User entity)
		{
            _db.Users.Update(entity);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch(Exception ex) 
            { var a = ex; }
            return entity;
		}

        public async Task<User> GetByEmail(string Email)
        {
            var a = await _db.Users.FirstOrDefaultAsync(u=>u.Email==Email);
            return a;
        }

        public async Task<User> GetById(int id)
        {
            var a = _db.Users;
             var b =  await a.FirstOrDefaultAsync(u => u.Id == id);
            return b;
        }

        public IEnumerable<User> Get()
        {
            return _db.Users;
        }
    }
}
