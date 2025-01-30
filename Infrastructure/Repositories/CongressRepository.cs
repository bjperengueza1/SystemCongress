using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CongressRepository : ICongressRepository
{
    
    private readonly CongressContext _context;
    
    public CongressRepository(CongressContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Congress>> GetAllAsync()
    {
        return await _context.Congresses.ToListAsync();
    }

    public async Task<Congress> GetByIdAsync(int id)
    {
        return await _context.Congresses.FindAsync(id);
    }

    public async Task AddAsync(Congress entity)
    {
        entity.Guid = GuidHelper.GenerateGuid();
        await _context.Congresses.AddAsync(entity);
    }

    public void UpdateAsync(Congress entity)
    {
        _context.Congresses.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteAsync(Congress entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<PaginatedResult<Congress>> GetPagedAsync(int pageNumber, int pageSize, string search)
    {
        IQueryable<Congress> query = _context.Congresses;
        
        if(!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c => c.Name.Contains(search));
        }
        
        //order desc
        query = query.OrderByDescending(c => c.CongressId);
        
        var congresses = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var totalCongresses = await query.CountAsync();

        return PaginatedResult<Congress>.Create(congresses, totalCongresses, pageNumber, pageSize);
        
    }

    public async Task<Congress> GetByGuidAsync(string guid)
    {
        return await _context.Congresses.FirstOrDefaultAsync(c => c.Guid == guid && c.StartDate > DateTime.Now);
    }

    public async Task<IEnumerable<CongressCertificate>> GetCertificatesByDniAsync(string dni)
    {
        //seleccionar todos los congresos
        var congresses = await _context.Congresses
            .ToListAsync();
        
        var certificates = new List<CongressCertificate>();
        
        //iterar sobre los congresos
        foreach (var congress in congresses)
        {
            var certificate = new CongressCertificate
            {
                CongressId = congress.CongressId,
                Name = congress.Name,
                StartDate = congress.StartDate,
                EndDate = congress.EndDate,
                Location = congress.Location
            };
            //seleccionar las exposiciones de ese congreso que tengan algun autor con ese dni
            /*var exposures = await _context.Exposures
                .Include(e => e.Congress)
                .Include(e => e.ExposureAuthor)
                .ThenInclude(ea => ea.Author)
                .Where(e => e.ExposureAuthor.Any(ea => ea.Author.IDNumber == dni) && e.CongressId == congress.CongressId)
                .ToListAsync();
            
            //si hay exposiciones
            if (exposures.Count > 0)
            {
                certificate.Exposures = exposures;
            }*/
            
            //seleccionar las asistencias que tenda aun Attendee con ese dni
            var attendances = await _context.Attendances
                .Include(a => a.Attendee)
                .Include(a => a.Exposure)
                .Where(a => a.Attendee.IDNumber == dni && a.Exposure.CongressId == congress.CongressId)
                .ToListAsync();
            
            //si hay asistencias
            if (attendances.Count > 0)
            {
                certificate.CertificateAttendance = true;
            }
            certificates.Add(certificate);
        }

        return certificates;
    }
}