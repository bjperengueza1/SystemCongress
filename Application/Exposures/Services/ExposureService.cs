using Application.Congress.DTOs;
using Application.Exposures.DTOs;
using Application.Exposures.Interfaces;
using AutoMapper;
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
    
    public Task<IEnumerable<ExposureDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ExposureDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
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
}