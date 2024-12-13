using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class CongressContext : DbContext
{
    public CongressContext(DbContextOptions<CongressContext> options) : base(options)
    { }
    
    public DbSet<Congresso> Congresses { get; set; }
    
    public DbSet<Exposure> Exposures { get; set; }
    
    public DbSet<Author> Authors { get; set; }
    
    public DbSet<Attendance> Attendances { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    // Método OnModelCreating para configuraciones avanzadas
    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración para convertir Enum a string
        modelBuilder.Entity<Exposure>()
            .Property(e => e.StatusExposure)
            .HasConversion<string>(); // Convierte el enum a string al guardar/leer
    }*/
    
}
