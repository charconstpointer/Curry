using Curry.Models;
using Curry.Persistence.Repository;
using System.Threading.Tasks;

namespace Curry.Services
{
    public class UserService
    {
        private readonly UserRepository userRepository;
        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<User> Authenticate(User user)
        {
            var res = await userRepository.GetUserByName(user.Name);
            if(user.Password == res.Password)
            {
                return res;
            }
            return null;
        }
    }
}
