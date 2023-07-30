using Microsoft.EntityFrameworkCore;
using YourProjectName.Models;
using Microsoft.EntityFrameworkCore;
using YourProjectName.Models;


namespace YourProjectName.Data
{
    public class YourDbContext : DbContext
    {
        public YourDbContext(DbContextOptions<YourDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
