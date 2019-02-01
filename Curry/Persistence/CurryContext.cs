using Curry.Models;
using Microsoft.EntityFrameworkCore;

namespace Curry.Persistence.Repository
{
    public class CurryContext : DbContext
    {
        public CurryContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        
    }
}
