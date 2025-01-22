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

        CreateMap<ExposureInsertDto, Exposure>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors)); // Mapea la colección de Authors

        CreateMap<Exposure, ExposureWitchAuthorsDto>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
            .ForMember(dest => dest.CongressName, opt => opt.MapFrom(src => src.Congress.Name)); // Mapea la colección de Authors

        CreateMap<Exposure, ExposureUpdateStatusDto>();
        CreateMap<ExposureUpdateStatusDto, Exposure>();
        
        //Autores
        CreateMap<AuthorInsertDto, Author>()
            .ForMember(dest => dest.Exposure, opt => opt.Ignore()); // Ignorar la propiedad de navegación
        
        //Asistentes
        CreateMap<AttendeeInsertDto, Attendee>();
        CreateMap<AttendeeDto, AttendeeInsertDto>();
        CreateMap<Attendee, AttendeeDto>();
        
        //Asistencia
        CreateMap<AttendanceInsertDto, Attendance>();
        CreateMap<Attendance, AttendanceDto>();
        
        CreateMap<Author, AuthorDto>();
    }
}