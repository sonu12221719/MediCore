using IdentityServiceLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityServiceLibrary.Data
{
    public class IdentityDBContext:DbContext
    {
        public IdentityDBContext() { }
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e=>e.Role)
                .HasConversion<string>();
            
            modelBuilder.Entity<User>()
                .Property(e=>e.Role)
                .HasConversion<string>();
        }
    }
}
