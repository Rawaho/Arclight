using Arclight.Database.Character.Model;
using Arclight.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Arclight.Database.Character
{
    public class CharacterContext : DbContext
    {
        public DbSet<CharacterModel> Characters { get; set; }

        private readonly IDatabaseConfiguration configuration;

        public CharacterContext(IDatabaseConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseConfiguration(configuration, DatabaseType.Character);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterModel>(e =>
            {
                e.ToTable("character");

                e.HasKey(m => m.Id);

                e.HasIndex(m => new { m.AccountId, m.Index });
                e.HasIndex(m => m.Name)
                    .IsUnique();

                e.Property(m => m.Name)
                    .IsRequired();

                e.Property(m => m.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
