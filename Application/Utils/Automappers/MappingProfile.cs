using Application.Attendances.DTOs;
using Application.Attendees.DTOs;
using Application.Authors.DTOs;
using Application.Congresses.DTOs;
using Application.Exposures.DTOs;
using Application.Rooms.DTOs;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Utils.Automappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Users
        CreateMap<UserInsertDto, User>();
        CreateMap<UserUpdateDto, User>();
        CreateMap<User, UserDto>();
        CreateMap<User,UserLoggedDto>()
            .ForMember(dest => dest.Token, opt => opt.Ignore());
        
        //Congresos
        CreateMap<CongressInsertDto,Congress>();
        CreateMap<Congress,CongressDto>();
        CreateMap<CongressUpdateDto,Congress>();
        
        //Rooms
        CreateMap<RoomInsertDto, Room>();
        CreateMap<Room, RoomDto>();
        CreateMap<RoomUpdateDto, Room>();
        CreateMap<Room, RoomWithCongressDto>()
            .ForMember(dest => dest.CongressName, opt => opt.MapFrom(src => src.Congress.Name));
        
        //Exposiciones
        CreateMap<ExposureInsertFormDto, ExposureInsertDto>()
            .ForMember(dest => dest.Authors, opt => opt.Ignore()); // Lo manejamos manualmente

        CreateMap<ExposureInsertDto, Exposure>();

        CreateMap<Exposure, ExposureWitchAuthorsDto>()
            .ForMember(dest => dest.CongressName, opt => opt.MapFrom(src => src.Congress.Name))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.ExposureAuthor.Select(ea => new AuthorDto
            {
                AuthorId = ea.AuthorId,
                Name = ea.Author.Name,
                IDNumber = ea.Author.IDNumber,
                InstitutionalMail = ea.Author.InstitutionalMail,
                PersonalMail = ea.Author.PersonalMail,
                PhoneNumber = ea.Author.PhoneNumber,
                Country = ea.Author.Country,
                City = ea.Author.City,
                AcademicDegree = ea.Author.AcademicDegree,
                Position = ea.Position
            }).OrderBy(a => a.Position).ToList()));

        CreateMap<ExposureUpdateDto, Exposure>();
        
        CreateMap<Exposure, ExposureUpdateStatusDto>();
        CreateMap<ExposureUpdateStatusDto, Exposure>();
        
        CreateMap<Exposure, ExposureApproveDto>();
        CreateMap<ExposureApproveDto, Exposure>();
        
        CreateMap<Exposure, ExposureRejectDto>();
        CreateMap<ExposureRejectDto, Exposure>();
        
        //Autores
        CreateMap<AuthorInsertDto, Author>();
        
        //Asistentes
        CreateMap<AttendeeInsertDto, Attendee>();
        CreateMap<AttendeeDto, AttendeeInsertDto>();
        CreateMap<Attendee, AttendeeDto>();
        
        //Asistencia
        CreateMap<AttendanceInsertDto, Attendance>();
        CreateMap<Attendance, AttendanceDto>();
        
        CreateMap<Author, AuthorDto>();

        CreateMap<CertificatesAttendance,CertificateAttendanceDto>()
            .ForMember(dest => dest.CertificatesAttendanceId, opt => opt.MapFrom(src => src.CertificatesAttendanceId))
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
            .ForMember(dest => dest.AttendeeName, opt => opt.MapFrom(src => src.Attendee.Name))
            .ForMember(dest => dest.AttendeeEmail, opt => opt.MapFrom(src => src.Attendee.Email))
            .ForMember(dest => dest.AttendeeIDNumber, opt => opt.MapFrom(src => src.Attendee.IDNumber))
            .ForMember(dest => dest.CongressName, opt => opt.MapFrom(src => src.Congress.Name))
            .ForMember(dest => dest.CongressStartDate, opt => opt.MapFrom(src => src.Congress.StartDate))
            .ForMember(dest => dest.CongressEndDate, opt => opt.MapFrom(src => src.Congress.EndDate));
    }
}