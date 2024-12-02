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
    
    public Task<IEnumerable<Congress>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Congress> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Congress entity)
    {
        await _context.Congresses.AddAsync(entity);
        //throw new NotImplementedException();
    }

    public void UpdateAsync(Congress entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync(Congress entity)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}