using AutoMapper;
using BAW_Project_API.Dtos;
using BAW_Project_API.Models;

namespace BAW_Project_API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AuthorDto, Author>();
            CreateMap<Author, AuthorDto>();
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
        }
    }
}
