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
    
    public async Task<IEnumerable<ExposureDto>> GetAllAsync()
    {
        var exposures = await _exposureRepository.GetAllAsync();
        
        return exposures.Select(c => _mapper.Map<ExposureDto>(c));
    }

    public async Task<ExposureDto> GetByIdAsync(int id)
    {
        var exposure = await _exposureRepository.GetByIdAsync(id);
        
        if (exposure == null) return null;
        
        return _mapper.Map<ExposureDto>(exposure);
    }

    public async Task<ExposureDto> CreateAsync(ExposureInsertDto ti)
    {
        var exposure = _mapper.Map<Exposure>(ti);
        
        await _exposureRepository.AddAsync(exposure);
        await _exposureRepository.SaveAsync();
        
        return _mapper.Map<ExposureDto>(exposure);

    }

    public Task<ExposureDto> UpdateAsync(int id, ExposureUpdateDto tu)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<ExposureDto>> GetPagedAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public async Task<ExposureDto> ChangeStatusAsync(int id, ExposureUpdateStatusDto exposureUpdateStatusDto)
    {
        //Traigo el objeto
        var congress = await _exposureRepository.GetByIdAsync(id);
        
        if (congress == null) return null;
        
        //Y lo que coincida lo actualizo
        congress = _mapper.Map(exposureUpdateStatusDto, congress);
        
        _exposureRepository.UpdateAsync(congress);
        
        return _mapper.Map<ExposureDto>(congress);
    }
}