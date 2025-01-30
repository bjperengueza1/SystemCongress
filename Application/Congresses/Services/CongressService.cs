using Application.Congresses.DTOs;
using Application.Congresses.Interfaces;
using Application.Files.Interfaces;
using AutoMapper;
using Domain.Common.Pagination;
using Domain.Interfaces;
using Domain.Entities;

namespace Application.Congresses.Services;

public class CongressService : ICongressService
{
    
    private readonly ICongressRepository _congressRepository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    
    public CongressService(
        ICongressRepository congressRepository,
        IFileService fileService,
        IMapper mapper)
    {
        _congressRepository = congressRepository;
        _fileService = fileService;
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

    public async Task<CongressDto> GetByGuidAsync(string guid)
    {
        var congress = await _congressRepository.GetByGuidAsync(guid);
        
        return congress == null ? null : _mapper.Map<CongressDto>(congress);
    }

    public async Task<IEnumerable<CongressCertificate>> GetCertificatesByDniAsync(string dni)
    {
        var congresses = await _congressRepository.GetCertificatesByDniAsync(dni);

        return congresses;
    }

    public async Task<Stream> DownloadCertificateAttendanceAsync(int congressId, string dni, string directorio)
    {
        var guid = Guid.NewGuid().ToString();
        var nameTemp = $"{dni}_{guid}.docx";

        await _fileService.CopyFileAsync("ASISTENTE.docx", nameTemp, directorio);
        
        // Reemplaza el texto en el archivo
        _fileService.ReplaceTextInWord(nameTemp,directorio,"NOMBRESAPELLIDOS","OMAR");
        
        _fileService.ConvertToPdf(nameTemp, directorio);
        var certificado = await _fileService.GetFileAsync($"{dni}_{guid}.pdf", directorio);

        //delete temp files
        await _fileService.DeleteFileAsync(nameTemp, directorio);
        await _fileService.DeleteFileAsync($"{dni}_{guid}.pdf", directorio);
        
        return certificado;
        
    }
}