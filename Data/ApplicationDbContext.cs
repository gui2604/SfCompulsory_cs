using Microsoft.EntityFrameworkCore;
using SfCompulsory_cs.Models;

namespace SfCompulsory_cs.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Nome da tabela em maiúsculas para Oracle
            modelBuilder.Entity<User>().ToTable("USERS");
            modelBuilder.Entity<Log>().ToTable("LOGS");

            base.OnModelCreating(modelBuilder);
        }
    }
}
