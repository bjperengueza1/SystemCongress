using System.Diagnostics;
using System.Reflection;
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

    public async Task<byte[]> DownloadCertificateAttendanceAsync(int congressId, string dni)
    {
        var directorioPadre = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
        var carpetaPlantillas = Path.Combine(directorioPadre, "templates");
        var rutaPlantilla = Path.Combine(carpetaPlantillas, "ASISTENTE.docx");
        
        var guid = Guid.NewGuid().ToString();
        var rutaSalida = Path.Combine(carpetaPlantillas, guid+".docx");
        
        WordReplacer.ReplaceTextInWord(rutaPlantilla,rutaSalida,"NOMBRESAPELLIDOS","OMAR");
        
        // Verifica si el archivo existe
        if (!File.Exists(rutaPlantilla))
            throw new FileNotFoundException("La plantilla del certificado no se encontró.");
        
        const string libreOfficePath = "/usr/bin/soffice";
        
        // Verifica si libreoffice está instalado
        if (!File.Exists(libreOfficePath))
            throw new FileNotFoundException("LibreOffice no está instalado.");
        
        var rutaCertificados = Path.Combine(directorioPadre, "certificados");
        
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = libreOfficePath,
                Arguments = $"--headless --convert-to pdf:writer_pdf_Export --outdir {rutaCertificados} {rutaSalida}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        
        process.Start();
        
        await process.WaitForExitAsync();

        var rutaCertificadoPdf = rutaCertificados + guid + ".pdf";
        
        if (!File.Exists(rutaCertificadoPdf))
            throw new FileNotFoundException("El certificado no se generó correctamente.");
        
        //Lee el archivo generado
        var file = await File.ReadAllBytesAsync(rutaCertificadoPdf);
        
        //Elimina el archivo generado
        File.Delete(rutaCertificadoPdf);
        
        return file;
    }
}