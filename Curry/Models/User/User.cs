using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Curry.Models.Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(64)]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }

    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        
    }
    
    public class UserRole
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
