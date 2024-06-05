using Microsoft.EntityFrameworkCore;
using ProjectFor7COMm.Data;
using ProjectFor7COMm.Models;

namespace ProjectFor7COMm.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task Add(User request)
        {
            await _context.Users.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User request)
        {
            _context.Users.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
