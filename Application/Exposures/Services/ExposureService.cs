using Application.Authors.DTOs;
using Application.Common;
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
    private readonly IAuthorRepository _authorRepository;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    
    public ExposureService(
        IExposureRepository exposureRepository,
        IAuthorRepository authorRepository,
        IEmailService emailService,
        IMapper mapper)
    {
        _exposureRepository = exposureRepository;
        _authorRepository = authorRepository;
        _emailService = emailService;
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
        
        foreach (var t in ti.Authors)
        {
            var author = await _authorRepository.GetByIdNumberAsync(t.IDNumber);
            if ( author == null)
            {
                author = _mapper.Map<Author>(t);
                await _authorRepository.AddAsync(author);
            }

            exposure.ExposureAuthor.Add(
                new ExposureAuthor
                {
                    Author = author
                });
        }

        await _authorRepository.SaveAsync();
        
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
        var exposure = await _exposureRepository.GetByIdAsync(id);
        
        if (exposure == null) return null;
        
        //Y lo que coincida lo actualizo
        exposure = _mapper.Map(exposureUpdateStatusDto, exposure);
        
        _exposureRepository.UpdateAsync(exposure);

        await _exposureRepository.SaveAsync();
        
        // Enviar correo al actualizar el estado
        var emailSent = await _emailService.SendStatusChangeNotificationAsync(exposure);
        
        return _mapper.Map<ExposureWitchAuthorsDto>(exposure);
    }

    public async Task<ExposureWitchAuthorsDto> GetByGuidAsync(string guid)
    {
        var exposure = await _exposureRepository.GetByGuidAsync(guid);
        
        return exposure == null ? null : _mapper.Map<ExposureWitchAuthorsDto>(exposure);
    }
}