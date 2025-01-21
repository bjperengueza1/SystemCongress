using Application.Attendees.DTOs;
using Application.Attendees.Interfaces;
using AutoMapper;
using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Attendees.Services;

public class AttendeeService : IAttendeeService
{
    
    private readonly IAttendeeRepository _attendeeRepository;
    private readonly IMapper _mapper;
    
    public AttendeeService(
        IAttendeeRepository attendeeRepository,
        IMapper mapper
    )
    {
        _attendeeRepository = attendeeRepository;
        _mapper = mapper;
    }
    public Task<IEnumerable<AttendeeDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<AttendeeDto> GetByIdAsync(int id)
    {
        var attendee = await _attendeeRepository.GetByIdAsync(id);
        return attendee == null ? null : _mapper.Map<AttendeeDto>(attendee);
    }

    public async Task<AttendeeDto> CreateAsync(AttendeeInsertDto ti)
    {
        var attendee = _mapper.Map<Attendee>(ti);
        await _attendeeRepository.AddAsync(attendee);
        await _attendeeRepository.SaveAsync();
        return _mapper.Map<AttendeeDto>(attendee);
    }

    public Task<AttendeeDto> UpdateAsync(int id, AttendeeUpdateDto tu)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<AttendeeDto>> GetPagedAsync(int pageNumber, int pageSize, string search)
    {
        throw new NotImplementedException();
    }
    
    public async Task<AttendeeDto> GetAttendeeByIdNumberAsync(string idNumber)
    {
        var attendee = await _attendeeRepository.GetAttendeeByIdNumberAsync(idNumber);
        
        return attendee == null ? null : _mapper.Map<AttendeeDto>(attendee);
    }
}