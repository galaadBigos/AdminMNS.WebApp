using AdminMNS.WebApp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminMNS.WebApp.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<GraduatingClass> GraduatingClass { get; set; }

        public AppDbContext(DbContextOptions options) : base (options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(u => u.GraduatingClass)
                .WithMany(gc => gc.Users)
                .HasForeignKey(u => u.GraduatingClassId);
        }
    }
}
