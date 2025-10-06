using ApiChallenge.Data.Entities.Dtos;
using AutoMapper;

namespace ApiChallenge.Data.Entities;

public class UserMappers : Profile
{
    public UserMappers()
    {
        CreateMap<CreateUserDto, User>().ReverseMap();
        CreateMap<UpdateUserDto, User>().ReverseMap();
        CreateMap<User, UserResponseDto>().ReverseMap();
        CreateMap<User, UserWithAddressResponseDto>().ReverseMap();
    }
}