using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories;

public class ExposureRepository : IExposureRepository
{
    private readonly CongressContext _context;
    
    public ExposureRepository(CongressContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Exposure>> GetAllAsync()
    {
        return await _context.Exposures.ToListAsync();
    }

    public async Task<Exposure> GetByIdAsync(int id)
    {
        return await _context.Exposures
            .Include(e => e.Authors)
            .FirstOrDefaultAsync(e => e.ExposureId == id);
    }

    public async Task AddAsync(Exposure entity)
    {
        await _context.Exposures.AddAsync(entity);
        
        //var authors = entity.Authors;
        //recorrer autores e insertarlos en la tabla de autores
        /*foreach (var author in authors)
        {
            var authorEntity = new Author
            {
                Name = author.Name,
                LastName = author.LastName
            };
            _context.Authors.Add(authorEntity);
        }*/
        
        //throw new NotImplementedException();
    }

    public void UpdateAsync(Exposure entity)
    {
        _context.Exposures.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteAsync(Exposure entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}