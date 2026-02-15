using Microsoft.EntityFrameworkCore;
using RoomReserve.Api.Models;

namespace RoomReserve.Api.Data;

public class AppDbContext : DbContext
{
    // Constructor untuk runtime (dengan options)
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    // Constructor tanpa parameter untuk design-time (migration)
    public AppDbContext()
    {
    }
    
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Fallback connection string untuk migration
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RoomReserveDb;Trusted_Connection=true;TrustServerCertificate=true;");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Room>().HasQueryFilter(r => r.DeletedAt == null);
        modelBuilder.Entity<Room>().HasIndex(r => r.Code).IsUnique();
        
        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, Code = "R101", Name = "Ruang Seminar A", Capacity = 50, Location = "Gedung A Lantai 1", Status = RoomStatus.Available, CreatedAt =  new DateTime(2026, 2, 15, 0, 0, 0, DateTimeKind.Utc)},
            new Room { Id = 2, Code = "R102", Name = "Ruang Kelas 201", Capacity = 40, Location = "Gedung B Lantai 2", Status = RoomStatus.Available, CreatedAt = new DateTime(2026, 2, 15, 0, 0, 0, DateTimeKind.Utc)},
            new Room { Id = 3, Code = "LAB01", Name = "Lab Komputer", Capacity = 30, Location = "Gedung C Lantai 1", Status = RoomStatus.Available, CreatedAt =  new DateTime(2026, 2, 15, 0, 0, 0, DateTimeKind.Utc)}
        );
    }
}