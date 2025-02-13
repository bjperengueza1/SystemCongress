using Application.Congresses.DTOs;
using Application.Congresses.Interfaces;
using Application.Files.Interfaces;
using AutoMapper;
using Domain.Common.Pagination;
using Domain.Dtos;
using Domain.Interfaces;
using Domain.Entities;
using Newtonsoft.Json;

namespace Application.Congresses.Services;

public class CongressService : ICongressService
{
    
    private readonly ICongressRepository _congressRepository;
    private readonly IExposureRepository _exposureRepository;
    private readonly IAttendeeRepository _attendeeRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    
    public CongressService(
        ICongressRepository congressRepository,
        IExposureRepository exposureRepository,
        IAttendeeRepository attendeeRepository,
        IAuthorRepository authorRepository,
        IFileService fileService,
        IMapper mapper)
    {
        _congressRepository = congressRepository;
        _exposureRepository = exposureRepository;
        _attendeeRepository = attendeeRepository;
        _authorRepository = authorRepository;
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
        var attendee = await _attendeeRepository.GetAttendeeByIdNumberAsync(dni);
        var congress = await _congressRepository.GetByIdAsync(congressId);
        
        if (congress == null || attendee == null) return null;
        
        var guid = Guid.NewGuid().ToString("N");
        var nameTemp = $"{dni}_{guid}.docx";

        string[] pathTemplate = [directorio,congress.Guid];

        await _fileService.CopyFileAsync(congress.fileCertificateAttendance, nameTemp, pathTemplate);
        
        //lista de campos a reemplazar
        var fields = new Dictionary<string, string>
        {
            {"PERSONA", attendee.Name.ToUpper()}
        };
        
        //Reemplaza el texto en el archivo
        _fileService.ReplaceTextInWord(nameTemp,pathTemplate,fields);
        
        var namePdf = _fileService.ConvertToPdf(nameTemp, pathTemplate);
        var certificado = await _fileService.GetFileAsync(namePdf, pathTemplate);

        //delete temp files
        await _fileService.DeleteFileAsync(nameTemp, pathTemplate);
        await _fileService.DeleteFileAsync(namePdf, pathTemplate);
        
        return certificado;
    }

    public async Task<Stream> DownloadCertificateExposureAsync(int exposureId, string dni, string directorio)
    {
        var author = await _authorRepository.GetByIdNumberAsync(dni);
        var exposure = await _exposureRepository.GetByIdAsync(exposureId);
        
        if (exposure == null || author == null) return null;
        
        var guid = Guid.NewGuid().ToString("N");
        var nameTemp = $"{dni}_{guid}.docx";
        
        string[] pathTemplate = [directorio,exposure.Congress.Guid];
        
        await _fileService.CopyFileAsync(exposure.Congress.fileCertificateExposure, nameTemp, pathTemplate);
        
        //lista de campos a reemplazar
        var fields = new Dictionary<string, string>
        {
            {"PERSONA", author.Name.ToUpper()},
            {"TEMA", exposure.Name.ToUpper()}
        };
        
        //Reemplaza el texto en el archivo
        _fileService.ReplaceTextInWord(nameTemp,pathTemplate,fields);
        
        var namePdf = _fileService.ConvertToPdf(nameTemp, pathTemplate);
        var certificado = await _fileService.GetFileAsync(namePdf, pathTemplate);

        //delete temp files
        await _fileService.DeleteFileAsync(nameTemp, pathTemplate);
        await _fileService.DeleteFileAsync(namePdf, pathTemplate);
        
        return certificado;
    }

    public async Task<Stream> DownloadCertificateConferenceAsync(int exposureId, string dni, string directorio)
    {
        var author = await _authorRepository.GetByIdNumberAsync(dni);
        var exposure = await _exposureRepository.GetByIdAsync(exposureId);
        
        if (exposure == null || author == null) return null;
        
        var guid = Guid.NewGuid().ToString("N");
        var nameTemp = $"{dni}_{guid}.docx";
        
        string[] pathTemplate = [directorio,exposure.Congress.Guid];
        
        await _fileService.CopyFileAsync(exposure.Congress.fileCertificateConference, nameTemp, pathTemplate);
        
        //lista de campos a reemplazar
        var fields = new Dictionary<string, string>
        {
            {"PERSONA", author.Name.ToUpper()},
            {"TEMA", exposure.Name.ToUpper()}
        };
        
        //Reemplaza el texto en el archivo
        _fileService.ReplaceTextInWord(nameTemp,pathTemplate,fields);
        
        var namePdf = _fileService.ConvertToPdf(nameTemp, pathTemplate);
        var certificado = await _fileService.GetFileAsync(namePdf, pathTemplate);

        //delete temp files
        await _fileService.DeleteFileAsync(nameTemp, pathTemplate);
        await _fileService.DeleteFileAsync(namePdf, pathTemplate);
        
        return certificado;
    }

    public async Task<CongressDto> ActiveAsync(int id)
    {
        var congress = await _congressRepository.GetByIdAsync(id);
        
        if (congress == null) return null;
        
        //get congresses active
        var congressActive = await _congressRepository.GetActiveAsync();
        
        //set inactive all congresses
        congressActive.Status = false; 
        _congressRepository.UpdateAsync(congressActive);
        
        //active congress
        congress.Status = true;
        
        _congressRepository.UpdateAsync(congress);
        await _congressRepository.SaveAsync();
        
        return _mapper.Map<CongressDto>(congress);
    }

    public async Task<CongressDto> GetActivesAsync()
    {
        var congress = await _congressRepository.GetActiveAsync();
        
        return congress == null ? null : _mapper.Map<CongressDto>(congress);
    }

    public async Task<bool> FileCertificateAttendance(int id, string fileName)
    {
        var congress = await _congressRepository.GetByIdAsync(id);
        
        if (congress == null) return false;
        
        congress.fileCertificateAttendance = fileName;
        
        _congressRepository.UpdateAsync(congress);
        
        await _congressRepository.SaveAsync();
        
        return true;
    }

    public async Task<bool> FileCertificateExposure(int id, string fileName)
    {
        var congress = await _congressRepository.GetByIdAsync(id);
        
        if (congress == null) return false;
        
        congress.fileCertificateExposure = fileName;
        
        _congressRepository.UpdateAsync(congress);
        
        await _congressRepository.SaveAsync();
        
        return true;
    }
    
    public async Task<bool> FileCertificateConference(int id, string fileName)
    {
        var congress = await _congressRepository.GetByIdAsync(id);
        
        if (congress == null) return false;
        
        congress.fileCertificateConference = fileName;
        
        _congressRepository.UpdateAsync(congress);
        
        await _congressRepository.SaveAsync();
        
        return true;
    }

    public async Task<bool> FileFlayer(int id, string fileName)
    {
        var congress = await _congressRepository.GetByIdAsync(id);
        
        if (congress == null) return false;
        
        congress.fileFlayer = fileName;
        
        _congressRepository.UpdateAsync(congress);
        
        await _congressRepository.SaveAsync();
        
        return true;
    }
}