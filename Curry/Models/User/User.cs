using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Curry.Models.User
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(64)]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        [JsonIgnore]
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
