using ApiChallenge.Data.Entities.Dtos;
using AutoMapper;

namespace ApiChallenge.Data.Entities;

public class UserMappers : Profile
{
    public UserMappers()
    {
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
        CreateMap<User, UserResponseDto>();
        CreateMap<User, UserWithAddressResponseDto>();
    }
}