using ApiClimate.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiClimate.Data
{
    public class ClimateDbContext : DbContext
    {
        public ClimateDbContext()
        {
        }

        public ClimateDbContext(DbContextOptions<ClimateDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UF>().HasKey(c => new { c.Id });
            modelBuilder.Entity<City>().HasKey(c => new { c.Id });
            modelBuilder.Entity<Region>().HasKey(c => new { c.Id });
            modelBuilder.Entity<ConditionClimate>().HasKey(c => new { c.Id });
            modelBuilder.Entity<LocationClimate>().HasKey(c => new { c.Id });
            modelBuilder.Entity<TokenAplicacao>().HasKey(c => new { c.Id });
            modelBuilder.Entity<UserAplication>().HasKey(c => new { c.Id });
        }

        public virtual DbSet<UF> UF { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<ConditionClimate> ConditionClimate { get; set; }
        public virtual DbSet<LocationClimate> LocationClimate { get; set; }
        public virtual DbSet<TokenAplicacao> TokenAplicacao { get; set; }
        public virtual DbSet<UserAplication> UserAplication { get; set; }
    }
}
