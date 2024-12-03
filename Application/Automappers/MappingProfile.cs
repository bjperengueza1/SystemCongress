using Application.Congress.DTOs;
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
    }
}