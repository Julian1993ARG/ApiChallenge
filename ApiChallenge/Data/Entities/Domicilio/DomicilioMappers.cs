using ApiChallenge.Data.Entities.Dtos;
using AutoMapper;

namespace ApiChallenge.Data.Entities;

public class DomicilioMappers : Profile
{
    public DomicilioMappers()
    {
        CreateMap<CreateDomicilioDto, Domicilio>().ReverseMap();
        CreateMap<CreateDomicilioForUserDto, Domicilio>()
            .ForMember(dest => dest.UsuarioId, opt => opt.Ignore()) // Se asignará manualmente
            .ReverseMap();
        CreateMap<UpdateDomicilioDto, Domicilio>().ReverseMap();
        CreateMap<Domicilio, DomicilioResponseDto>().ReverseMap();
    }
}