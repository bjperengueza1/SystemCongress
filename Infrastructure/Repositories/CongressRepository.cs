using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Domain.Repositories;

public class CongressRepository : ICongressRepository
{
    
    private readonly CongressContext _context;
    
    public CongressRepository(CongressContext context)
    {
        _context = context;
    }
    
    public Task<IEnumerable<Congresso>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Congresso> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Congresso entity)
    {
        await _context.Congresses.AddAsync(entity);
        //throw new NotImplementedException();
    }

    public void UpdateAsync(Congresso entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync(Congresso entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}