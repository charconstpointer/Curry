using System.Threading.Tasks;
using Curry.Helpers;
using Curry.Models.User;
using Curry.Persistence.Repository;

namespace Curry.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository repository)
        {
            _userRepository = repository;
        }

//IIIIIIIIIIIIIIIIIIIIIIIIIIII
        public async Task<User> Authenticate(User user)
        {
            var res = await _userRepository.FindUserByName(user.Username);
            var passwordHash = Crypto.Hash(user.Password, res.Salt);
            return passwordHash.password == res.Password ? res : null;
        }

        public async Task<User> AddUserAsync(User user)
        {
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<User> FindUserByName(string name)
        {
            return await _userRepository.FindUserByName(name);
        }

        public async Task<User> FindUserById(int id)
        {
            return await _userRepository.FindUserById(id);
        }
    }
}