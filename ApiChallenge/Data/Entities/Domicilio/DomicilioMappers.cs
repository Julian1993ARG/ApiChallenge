using ApiChallenge.Data.Entities.Dtos;
using AutoMapper;

namespace ApiChallenge.Data.Entities;

public class DomicilioMappers : Profile
{
    public DomicilioMappers()
    {
        CreateMap<CreateAddressDto, Domicilio>().ReverseMap();
        CreateMap<CreateAddressDto, Domicilio>()
            .ForMember(dest => dest.UsuarioId, opt => opt.Ignore()) // Se asignará manualmente
            .ReverseMap();
        CreateMap<Domicilio, DomicilioResponseDto>().ReverseMap();
        CreateMap<Domicilio, CreateOrAlterAddressForUserDto>().ReverseMap();
    }
}