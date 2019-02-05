using Curry.Models;
using Curry.Persistence.Repository;
using System.Threading.Tasks;
using Curry.Helpers;

namespace Curry.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository repository)
        {
            _userRepository = repository;
        }
        public async Task<User> Authenticate(User user)
        {
            var res = await _userRepository.FindUserByName(user.Name);
            var passwordHash = Crypto.Hash(user.Password, res.Salt);
            return passwordHash.Item1 == res.Password ? res : null;
        }

        public async Task<User> AddUserAsync(User user)
        {
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<User> FindUserByName(string name)
        {
            return await _userRepository.FindUserByName(name);
        }
    }
}
