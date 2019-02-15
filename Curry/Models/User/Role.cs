using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Curry.Models.User
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Role
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        // ReSharper disable once CollectionNeverUpdated.Global
        public ICollection<UserRole> UserRoles { get; set; }
        
    }
}