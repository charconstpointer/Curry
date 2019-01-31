using Curry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curry.Auth
{
    public interface ITokenFactory<T>
    {
        T GenerateToken(User user);
    }
}
