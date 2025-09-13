using GameZone.Models;

namespace GameZone.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<GameDevice>()
                    .HasKey(e => new { e.DeviceId, e.GameId });
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<GameDevice> GameDevices { get; set; }
}