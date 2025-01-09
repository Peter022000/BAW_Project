using AutoMapper;
using BAW_Project_API.Dtos;
using BAW_Project_API.Entities;

namespace BAW_Project_API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
