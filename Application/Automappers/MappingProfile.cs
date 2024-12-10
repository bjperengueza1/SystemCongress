using Application.Author.DTOs;
using Application.Congress.DTOs;
using Application.Exposures.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Automappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CongressInsertDto,Congresso>();
        CreateMap<Congresso,CongressDto>();
        CreateMap<CongressUpdateDto,Congresso>();
        
        CreateMap<ExposureInsertDto, Exposure>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameExposure )); // Mapea la colección de Authors

        CreateMap<Exposure, ExposureDto>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors)); // Mapea la colección de Authors
        
        CreateMap<AuthorInsertDto, Domain.Entities.Author>()
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