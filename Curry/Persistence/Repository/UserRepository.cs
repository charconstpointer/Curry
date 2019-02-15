using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Curry.Helpers;
using Curry.Models.User;

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
            var (password, salt) = Crypto.Hash(user.Password);
            user.Password = password;
            user.Salt = salt;
            var res = await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return res.Entity;
        }
        public async Task<User> FindUserByName(string name)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == name);
        }

        public async Task<User> FindUserById(int id)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync();
            return user;
        }
    }
}
