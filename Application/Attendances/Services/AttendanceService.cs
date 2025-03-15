using Application.Attendances.Interfaces;
using Application.Attendances.DTOs;
using Application.Files.Interfaces;
using AutoMapper;
using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Filter;
using Domain.Interfaces;

namespace Application.Attendances.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    
    public AttendanceService(
        IAttendanceRepository attendanceRepository,
        IFileService fileService,
        IMapper mapper)
    {
        _attendanceRepository = attendanceRepository;
        _fileService = fileService;
        _mapper = mapper;
    }
    
    public Task<IEnumerable<AttendanceDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<AttendanceDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<AttendanceDto> CreateAsync(AttendanceInsertDto ti)
    {
        var attendee = _mapper.Map<Attendance>(ti);
        await _attendanceRepository.AddAsync(attendee);
        await _attendanceRepository.SaveAsync();

        return _mapper.Map<AttendanceDto>(attendee);
    }

    public Task<AttendanceDto> UpdateAsync(int id, AttendanceUpdateDto tu)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedResult<AttendanceDto>> GetPagedAsync(AttendanceFilter tf)
    {
        var pagedData = await _attendanceRepository.GetPagedAsync(tf);
        
        return pagedData.Map(_mapper.Map<AttendanceDto>);
    }

    public async Task<AttendanceDto> GetByAttendeeIdAndExposureIdAsync(int attendeeId, int exposureId)
    {
        var attendance = await _attendanceRepository.GetByAttendeeIdAndExposureIdAsync(attendeeId, exposureId);
        
        return _mapper.Map<AttendanceDto>(attendance);
    }

    public async Task<Stream> GetReportExcelAsync(AttendanceFilter filter)
    {
        var data = await _attendanceRepository.GetAllEAsync(filter);
        
        var dataTransformed = data.Select(x => new List<string>
        {
            x.Attendee.Name,
            x.Attendee.IDNumber,
            x.Date.ToString(),
            x.Exposure.Name,
            x.Exposure.Congress.Name
        }).ToList();
        
        var headers = new List<string>
        {
            "Nombre",
            "Documento Identidad",
            "Fecha Asistencia",
            "Exposición",
            "Congreso"
        };
        
        var excel = await _fileService.CreateExcelStream(headers, dataTransformed);

        return excel;
    }
}