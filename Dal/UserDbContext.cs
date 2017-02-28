using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace authorization
{
    public class UserDbContext : DbContext
    {
        private readonly ILoggerFactory loggerFactory;

        public UserDbContext(DbContextOptions<UserDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            this.loggerFactory = loggerFactory;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(loggerFactory);

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(r => r.Id);

            modelBuilder.Entity<UserClaim>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<UserClaim>().HasKey(r => r.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
