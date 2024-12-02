using Application.Congress.DTOs;
using Application.Congress.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Entities;

namespace Application.Congress.Services;

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

    public async Task<CongressDto> CreateAsync(CongressInsertDto insertDto)
    {
        var congress = _mapper.Map<Congresso>(insertDto);
        
        await _congressRepository.AddAsync(congress);
        await _congressRepository.SaveAsync();
        
        return _mapper.Map<CongressDto>(congress);
    }
}