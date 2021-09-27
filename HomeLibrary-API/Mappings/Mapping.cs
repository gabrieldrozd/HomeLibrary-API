using AutoMapper;
using HomeLibrary_API.Data.Models;
using HomeLibrary_API.DTOs.Author;
using HomeLibrary_API.DTOs.Book;

namespace HomeLibrary_API.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Author, AuthorCreateDTO>().ReverseMap();
            CreateMap<Author, AuthorUpdateDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, BookCreateDTO>().ReverseMap();
            CreateMap<Book, BookUpdateDTO>().ReverseMap();
        }
    }
}
