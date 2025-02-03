using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class CongressContext : DbContext
{
    public CongressContext(DbContextOptions<CongressContext> options) : base(options)
    { }
    
    public DbSet<Congress> Congresses { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Exposure> Exposures { get; set; }
    public DbSet<Author?> Authors { get; set; }
    public DbSet<ExposureAuthor> ExposureAuthors { get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<User?> Users { get; set; }
}
