using Arclight.Database.Auth.Model;
using Arclight.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Arclight.Database.Auth
{
    public class AuthContext : DbContext
    {
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<ServerModel> Servers { get; set; }

        private readonly IDatabaseConfiguration configuration;

        public AuthContext(IDatabaseConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseConfiguration(configuration, DatabaseType.Auth);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>(e =>
            {
                e.ToTable("account");

                e.HasKey(m => m.Id);

                e.HasIndex(m => m.Username)
                    .IsUnique();
                e.HasIndex(m => new { m.Id, m.SessionKey });

                e.Property(m => m.Username)
                    .IsRequired()
                    .HasMaxLength(32);
                e.Property(m => m.Password)
                    .IsRequired()
                    .HasMaxLength(128);
                e.Property(m => m.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<AccountCharacterCountModel>(e =>
            {
                e.ToTable("account_character_count");

                e.HasKey(m => m.Id);

                e.HasOne(m => m.Account)
                    .WithMany(m => m.CharacterCounts)
                    .HasForeignKey(m => m.Id);
                e.HasOne(m => m.Server)
                    .WithMany(m => m.CharacterCounts)
                    .HasForeignKey(m => m.ServerId);
            });

            modelBuilder.Entity<ServerModel>(e =>
            {
                e.ToTable("server");

                e.HasKey(m => m.Id);

                e.Property(m => m.Name)
                    .IsRequired()
                    .HasMaxLength(32);
                e.Property(m => m.Host)
                    .IsRequired()
                    .HasMaxLength(64);

                // seeded data
                e.HasData(new ServerModel
                {
                    Id   = 1,
                    Name = "Arclight Server",
                    Host = "127.0.0.1",
                    Port = 10100,
                });
            });

            modelBuilder.Entity<ServerClusterModel>(e =>
            {
                e.ToTable("server_cluster");

                e.HasKey(m => new { m.Id, m.Index });

                e.HasOne(m => m.Server)
                    .WithMany(m => m.Nodes)
                    .HasForeignKey(m => m.Id);

                e.Property(m => m.Host)
                    .IsRequired()
                    .HasMaxLength(64);

                // seeded data
                e.HasData(new ServerClusterModel
                {
                    Id    = 1,
                    Index = 1,
                    Host  = "127.0.0.1",
                    Port  = 10200
                });
            });
        }
    }
}
