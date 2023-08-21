using Microsoft.EntityFrameworkCore;
using VirtualMind.Test.Library.Model;

namespace VirtualMind.Test.EF.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
