using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class CongressContext : DbContext
{
    public CongressContext(DbContextOptions<CongressContext> options) : base(options)
    { }
    
    public DbSet<Congresso> Congresses { get; set; }
    
}
