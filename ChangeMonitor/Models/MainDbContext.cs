using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ChangeMonitor.Models {
    public class MainDbContext: DbContext {

        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options) {
        }
        public DbSet<Testdata> Testdatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Filename=TestDatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Map table names
            modelBuilder.Entity<Testdata>().ToTable("Testdata", "test");
            modelBuilder.Entity<Testdata>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreateDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
