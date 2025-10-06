using ApiChallenge.Data.Entities.Dtos;
using AutoMapper;

namespace ApiChallenge.Data.Entities;

public class UserMappers : Profile
{
    public UserMappers()
    {
        CreateMap<CreateUserDto, User>().ReverseMap();
        CreateMap<CreateUserWithAddressDto, User>()
            .ForMember(dest => dest.Domicilios, opt => opt.Ignore()) // Se manejará manualmente en el servicio
            .ReverseMap();
        CreateMap<UpdateUserDto, User>().ReverseMap();
        CreateMap<User, UserResponseDto>().ReverseMap();
        CreateMap<User, UserWithAddressResponseDto>().ReverseMap();
    }
}