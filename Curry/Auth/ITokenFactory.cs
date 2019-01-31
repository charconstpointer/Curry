using Curry.Models;

namespace Curry.Auth
{
    public interface ITokenFactory<out T>
    {
        T GenerateToken(User user);
    }
}
