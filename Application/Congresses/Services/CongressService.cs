using Application.Congresses.DTOs;
using Application.Congresses.Interfaces;
using AutoMapper;
using Domain.Common.Pagination;
using Domain.Interfaces;
using Domain.Entities;

namespace Application.Congresses.Services;

public class CongressService : ICongressService
{
    
    private readonly ICongressRepository _congressRepository;
    private readonly IMapper _mapper;
    
    public CongressService(
        ICongressRepository congressRepository,
        IMapper mapper)
    {
        _congressRepository = congressRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<CongressDto>> GetAllAsync()
    {
        var congresses = await _congressRepository.GetAllAsync();
        
        return congresses.Select(c => _mapper.Map<CongressDto>(c));
    }
    
    public async Task<CongressDto> GetByIdAsync(int id)
    {
        var congress = await _congressRepository.GetByIdAsync(id);
        
        if (congress == null) return null;
        
        return _mapper.Map<CongressDto>(congress);
    }

    public async Task<CongressDto> CreateAsync(CongressInsertDto insertDto)
    {
        var congress = _mapper.Map<Congress>(insertDto);
        
        await _congressRepository.AddAsync(congress);
        await _congressRepository.SaveAsync();
        
        return _mapper.Map<CongressDto>(congress);
    }
    
    public async Task<CongressDto> UpdateAsync(int id, CongressUpdateDto updateDto)
    {
        //Traigo el objeto
        var congress = await _congressRepository.GetByIdAsync(id);
        
        if (congress == null) return null;
        
        //Y lo que coincida lo actualizo
        congress = _mapper.Map(updateDto, congress);
        
        _congressRepository.UpdateAsync(congress);

        await _congressRepository.SaveAsync();
        
        return _mapper.Map<CongressDto>(congress);
    }

    public async Task<PaginatedResult<CongressDto>> GetPagedAsync(int pageNumber, int pageSize, string search)
    {
        var pagedData = await _congressRepository.GetPagedAsync(pageNumber, pageSize, search);

        return pagedData.Map(c => _mapper.Map<CongressDto>(c));
    }
}