using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Curry.Models.User
{
    public class Role
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public ICollection<UserRole> UserRoles { get; set; }
        
    }
}