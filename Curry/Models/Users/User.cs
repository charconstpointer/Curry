using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public byte[] Salt { get; set; }
    }
}
