using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Curry.Models
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
    }
}
