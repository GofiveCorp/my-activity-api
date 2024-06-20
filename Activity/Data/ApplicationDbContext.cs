using Activity.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Activity.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Models.Entities.Activity> MyActivities { get; set; }
        public DbSet<ApiKey> MyApiKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Models.Entities.Activity>()
                .HasOne(a => a.ApiKey)
                .WithMany(k => k.Activities)
                .HasForeignKey(a => a.ApiKeyId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
