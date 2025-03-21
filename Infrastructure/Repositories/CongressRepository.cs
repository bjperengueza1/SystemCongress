using Domain.Common.Pagination;
using Domain.Dtos;
using Domain.Entities;
using Domain.Filter;
using Domain.Interfaces;
using Infrastructure.Data;
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
        entity.Guid = Guid.NewGuid().ToString();
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

    public async Task<PaginatedResult<Congress>> GetPagedAsync(CongressFilter tf)
    {
        IQueryable<Congress> query = _context.Congresses;
        
        if(!string.IsNullOrWhiteSpace(tf.search))
        {
            query = query.Where(c => c.Name.Contains(tf.search));
        }
        
        //order desc
        query = query.OrderByDescending(c => c.CongressId);
        
        var congresses = await query
            .Skip((tf.pageNumber - 1) * tf.pageSize)
            .Take(tf.pageSize)
            .ToListAsync();
        
        var totalCongresses = await query.CountAsync();
        
        return PaginatedResult<Congress>.Create(congresses, totalCongresses, tf.pageNumber, tf.pageSize);
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
        for (var index = 0; index < congresses.Count; index++)
        {
            var congress = congresses[index];
            var congressCertificate = new CongressCertificate
            {
                CongressId = congress.CongressId,
                Name = congress.Name,
                StartDate = congress.StartDate,
                EndDate = congress.EndDate,
                Location = congress.Location
            };
            
            //Traer solo la tabla de exposiciones sin incluir las tablas relacionadas
            IEnumerable<ExposureWithOutRelationsDto> expos = await _context.Exposures
                .Where(e => e.CongressId == congress.CongressId
                && e.ExposureAuthor.Any(ea => ea.Author.IDNumber == dni
                && e.Presented == "SI"))
                .Select(e => new ExposureWithOutRelationsDto
                {
                    ExposureId = e.ExposureId,
                    Name = e.Name,
                    StatusExposure = e.StatusExposure,
                    ResearchLine = e.ResearchLine,
                    Type = e.Type,
                    DateStart = e.DateStart,
                    DateEnd = e.DateEnd,
                    Guid = e.Guid
                })
                .ToListAsync();
            
            congressCertificate.Exposure = expos;

            //seleccionar las asistencias que tenda aun Attendee con ese dni
            var attendances = await _context.Attendances
                .Where(a => a.Attendee.IDNumber == dni && a.Exposure.CongressId == congress.CongressId)
                .ToListAsync();

            //si hay 4 asistencias
            if (attendances.Count >= congress.MinHours)
            {
                congressCertificate.CertificateAttendance = true;
                //validar si existe el certificado
                var certificateAttendance = await _context.CertificatesAttendances
                    .FirstOrDefaultAsync(ca =>
                        ca.CongressId == congress.CongressId && ca.AttendeeId == attendances.First().AttendeeId);
                if (certificateAttendance == null)
                {
                    //si no existe crear
                    await _context.CertificatesAttendances.AddAsync(new CertificatesAttendance()
                    {
                        CongressId = congress.CongressId,
                        AttendeeId = attendances.First().AttendeeId,
                        Guid = Guid.NewGuid().ToString()
                    });
                    
                    await _context.SaveChangesAsync();
                }
            }

            certificates.Add(congressCertificate);
        }

        return certificates;
    }

    public async Task<Congress> GetActiveAsync()
    {
        return await _context.Congresses.FirstOrDefaultAsync(c => c.Status);
    }

    public async Task<PaginatedResult<CertificatesAttendance>> GetCertificatesAttendanceAsync(CertificateAttendancesFilter tf)
    {
        var query = _context.CertificatesAttendances
            .Include(ca => ca.Congress)
            .Include(ca => ca.Attendee)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(tf.search))
        {
            query = query.Where(ca => ca.Attendee.Name.Contains(tf.search) || ca.Attendee.IDNumber.Contains(tf.search)
            || ca.Guid.Contains(tf.search));
        }
        
        query = query.OrderByDescending(ca => ca.CertificatesAttendanceId);
        
        var certificatesAttendance = await query
            .Skip((tf.pageNumber - 1) * tf.pageSize)
            .Take(tf.pageSize)
            .ToListAsync();
        
        var totalCertificatesAttendance = await query.CountAsync();
        
        return PaginatedResult<CertificatesAttendance>.Create(certificatesAttendance, totalCertificatesAttendance, tf.pageNumber, tf.pageSize);
    }
}