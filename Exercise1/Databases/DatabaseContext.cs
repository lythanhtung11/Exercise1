using Exercise1.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise1.Databases
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
