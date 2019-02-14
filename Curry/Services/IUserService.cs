using System.Threading.Tasks;
using Curry.Models.User;

namespace Curry.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(User user);
        Task<User> AddUserAsync(User user);
        Task<User> FindUserByName(string name);
        Task<User> FindUserById(int id);
    }
}