using Microsoft.EntityFrameworkCore;
using tectest1.Api.Domain.Models;

namespace tectest1.Api.Database
{
    public partial class DatabaseContext : DbContext
    {
        private readonly string _connectionString;
        public DatabaseContext()
        {
            _connectionString = Database?.GetConnectionString() ?? String.Empty;
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            _connectionString = Database?.GetConnectionString()??String.Empty;
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<MeterReadingUpload> MeterReadingUploads { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountId).ValueGeneratedNever();

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<MeterReadingUpload>(entity =>
            {
                entity.HasKey(e => e.MeterReadingUploadsId);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.MeterReadingUploads)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeterReadingUploads_Accounts");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
