using AutoMapper;
using LibraryMinimalApi.Models;
using LibraryMinimalApi.Models.DTOs;

namespace LibraryMinimalApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, CreateBookDTO>().ReverseMap();
            CreateMap<Book, UpdateBookDTO>().ReverseMap();
        }
    }
}
