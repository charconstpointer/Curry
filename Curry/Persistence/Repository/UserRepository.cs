using Curry.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Curry.Persistence.Repository
{
    public class UserRepository
    {
        private readonly CurryContext _context;
        public UserRepository(CurryContext context)
        {
            _context = context;
        }
        public async Task<User> AddUserAsync(User user)
        {
            var res = await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return res.Entity;
        }
        public async Task<User> GetUserByName(string name)
        {
            var res = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
            return res;
        }
    }
}
