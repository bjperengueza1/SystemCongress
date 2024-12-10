using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

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
        throw new NotImplementedException();
    }

    public async Task<Exposure> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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