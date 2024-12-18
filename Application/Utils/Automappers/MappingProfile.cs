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
        
        
        //Congresos
        CreateMap<CongressInsertDto,Congress>();
        CreateMap<Congress,CongressDto>();
        CreateMap<CongressUpdateDto,Congress>();
        
        //Rooms
        CreateMap<RoomInsertDto, Room>();
        CreateMap<Room, RoomDto>();
        CreateMap<RoomUpdateDto, Room>();
        
        //Exposiciones
        CreateMap<ExposureInsertFormDto, ExposureInsertDto>()
            .ForMember(dest => dest.Authors, opt => opt.Ignore()); // Lo manejamos manualmente

        CreateMap<ExposureInsertDto, Exposure>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameExposure )); // Mapea la colección de Authors

        CreateMap<Exposure, ExposureDto>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors)); // Mapea la colección de Authors
        
        //Autores
        CreateMap<AuthorInsertDto, Author>()
            .ForMember(dest => dest.Exposure, opt => opt.Ignore()); // Ignorar la propiedad de navegación
        
        CreateMap<Domain.Entities.Author, AuthorDto>();


    }
}

/*
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Francisco, Roberto>()
            .ForMember(dest => dest.MujeresRoberto, opt => opt.MapFrom(src => src.MujeresFrancisco ));

    }
}
*/