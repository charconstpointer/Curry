using Curry.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Curry.Helpers;

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
            var passwordHash = Crypto.Hash(user.Password);
            user.Password = passwordHash.Item1;
            user.Salt = passwordHash.Item2;
            var res = await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return res.Entity;
        }
        public async Task<User> FindUserByName(string name)
        {
            var res = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
            return res;
        }
    }
}
