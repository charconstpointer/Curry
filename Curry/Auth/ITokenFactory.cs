using Curry.Models.User;

namespace Curry.Auth
{
    public interface ITokenFactory<out T>
    {
        T GenerateToken(User user);
    }
}
