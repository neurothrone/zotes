using Microsoft.EntityFrameworkCore;
using Zotes.Persistence.Entities;

namespace Zotes.Persistence.Data;

public class ZotesAppDbContext(DbContextOptions<ZotesAppDbContext> options) : DbContext(options)
{
    public DbSet<ApiKeyEntity> ApiKeys => Set<ApiKeyEntity>();
    public DbSet<NoteEntity> Notes => Set<NoteEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApiKeyEntity>()
            .HasIndex(k => k.Key)
            .IsUnique();
    }
}