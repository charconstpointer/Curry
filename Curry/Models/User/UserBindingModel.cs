using System.Collections.Generic;

namespace Curry.Models.User
{
    public class UserBindingModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<int> Roles { get; set; }
    }
}