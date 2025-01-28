using Application.Attendances.Interfaces;
using Application.Attendances.DTOs;
using AutoMapper;
using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Attendances.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IMapper _mapper;
    
    public AttendanceService(IAttendanceRepository attendanceRepository,
        IMapper mapper)
    {
        _attendanceRepository = attendanceRepository;
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

    public async Task<PaginatedResult<AttendanceDto>> GetPagedAsync(int pageNumber, int pageSize, string search)
    {
        var pagedData = await _attendanceRepository.GetPagedAsync(pageNumber, pageSize, search);
        
        return pagedData.Map(_mapper.Map<AttendanceDto>);
    }

    public async Task<AttendanceDto> GetByAttendeeIdAndExposureIdAsync(int attendeeId, int exposureId)
    {
        var attendance = await _attendanceRepository.GetByAttendeeIdAndExposureIdAsync(attendeeId, exposureId);
        
        return _mapper.Map<AttendanceDto>(attendance);
    }
}