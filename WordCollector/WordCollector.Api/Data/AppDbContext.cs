
using Microsoft.EntityFrameworkCore;
using WordCollector.Shared;

namespace WordCollector.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<WordEntry> Words => Set<WordEntry>();
}
