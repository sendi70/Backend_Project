using ClientApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientApi.Data.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _context.Users.ToListAsync();
            return result;
        }
        public async Task<User> GetByNameAsync(string name)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Name == name);
            return result;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
