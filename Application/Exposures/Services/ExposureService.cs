using Application.Congresses.DTOs;
using Application.Exposures.DTOs;
using Application.Exposures.Interfaces;
using AutoMapper;
using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Exposures.Services;

public class ExposureService : IExposureService
{
    private readonly IExposureRepository _exposureRepository;
    private readonly IMapper _mapper;
    
    public ExposureService(
        IExposureRepository exposureRepository,
        IMapper mapper)
    {
        _exposureRepository = exposureRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ExposureWitchAuthorsDto>> GetAllAsync()
    {
        var exposures = await _exposureRepository.GetAllAsync();
        
        return exposures.Select(c => _mapper.Map<ExposureWitchAuthorsDto>(c));
    }

    public async Task<ExposureWitchAuthorsDto> GetByIdAsync(int id)
    {
        var exposure = await _exposureRepository.GetByIdAsync(id);
        
        if (exposure == null) return null;
        
        return _mapper.Map<ExposureWitchAuthorsDto>(exposure);
    }

    public async Task<ExposureWitchAuthorsDto> CreateAsync(ExposureInsertDto ti)
    {
        var exposure = _mapper.Map<Exposure>(ti);
        
        await _exposureRepository.AddAsync(exposure);
        await _exposureRepository.SaveAsync();
        
        return _mapper.Map<ExposureWitchAuthorsDto>(exposure);

    }

    public Task<ExposureWitchAuthorsDto> UpdateAsync(int id, ExposureUpdateDto tu)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedResult<ExposureWitchAuthorsDto>> GetPagedAsync(int pageNumber, int pageSize, string search)
    {
        var pagedData = await _exposureRepository.GetPagedAsync(pageNumber, pageSize, search);
        
        return pagedData.Map(p => _mapper.Map<ExposureWitchAuthorsDto>(p));
    }

    public async Task<ExposureWitchAuthorsDto> ChangeStatusAsync(int id, ExposureUpdateStatusDto exposureUpdateStatusDto)
    {
        //Traigo el objeto
        var congress = await _exposureRepository.GetByIdAsync(id);
        
        if (congress == null) return null;
        
        //Y lo que coincida lo actualizo
        congress = _mapper.Map(exposureUpdateStatusDto, congress);
        
        _exposureRepository.UpdateAsync(congress);
        
        return _mapper.Map<ExposureWitchAuthorsDto>(congress);
    }
}