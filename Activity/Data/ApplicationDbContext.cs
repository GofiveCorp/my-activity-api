using Activity.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Activity.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Models.Entities.Activity> Activities { get; set; }
        public DbSet<ConfigurationSetting> ConfigurationSettings { get; set; }

    }
}
