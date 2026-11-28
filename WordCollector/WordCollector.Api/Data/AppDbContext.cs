
using Microsoft.EntityFrameworkCore;
using WordCollector.Shared;

namespace WordCollector.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<WordEntry> Words => Set<WordEntry>();
    public DbSet<WordTypeEntry> WordTypes => Set<WordTypeEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WordTypeEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasData(
                Enum.GetValues<WordType>()
                    .Select(e => new WordTypeEntry
                    {
                        Id = e,
                        Name = e.ToString()
                    })
            );
        });

        modelBuilder.Entity<WordEntry>(entity =>
        {
            // Enforce unique entries on the Word column
            entity.HasIndex(w => w.Word)
                  .IsUnique();

            // Configure Type as a Foreign Key to WordTypes(Id)
            entity.HasOne(w => w.WordTypeEntry)
                  .WithMany()
                  .HasForeignKey(w => w.Type)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
