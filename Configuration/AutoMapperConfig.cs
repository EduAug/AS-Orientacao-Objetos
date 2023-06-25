using AutoMapper;
using AS_Orientacao_Objetos.Domain.DTOs;
using AS_Orientacao_Objetos.Domain.Entities;
using AS_Orientacao_Objetos.Domain.ViewModels;

namespace AS_Orientacao_Objetos.Configuration
{
    public class AutoMapperDTOs : Profile
    {
        public AutoMapperDTOs()
        {
            CreateMap<Genre, GenreDTO>().PreserveReferences().MaxDepth(1);
            CreateMap<User, UserDTO>().PreserveReferences().MaxDepth(1)
                .ForMember(dest=>dest.UserBooks, opt=>opt.MapFrom(src=>src.UserBooks))
                .ForMember(dest=>dest.DonatedBooks, opt=>opt.Ignore());
            CreateMap<Author, AuthorDTO>().PreserveReferences().MaxDepth(1)
                .ForMember(d=>d.AuthorBooks, opt=>opt.MapFrom(src=>src.AuthorBooks));
            CreateMap<Book, BookDTO>().PreserveReferences().MaxDepth(1)
                .ForMember(d=>d.GenreName, opt=>opt.MapFrom(src=>src.Genre.Name))
                .ForMember(d=>d.DonatorName, opt=>opt.MapFrom(src=>src.DonatedBy.Name))
                .ForMember(d=>d.AuthorBooks, opt=>opt.MapFrom(src=>src.AuthorBooks));
            CreateMap<AuthorBooks, AuthorBooksReferenceDTO>().PreserveReferences().MaxDepth(3)
                .ForMember(d=>d.AuthorId, opt=>opt.MapFrom(src=>src.AuthorId))
                .ForMember(d=>d.BookId, opt=>opt.MapFrom(src=>src.BookId));
            CreateMap<AuthorBooks, AuthorBooksDTO>().PreserveReferences().MaxDepth(3)
                .ForMember(d=>d.Author, opt=>opt.MapFrom(src=>src.Author.Name))
                .ForMember(d=>d.Book, opt=>opt.MapFrom(src=>src.Book.Title));
            CreateMap<UserBooks, UserBooksDTO>().PreserveReferences().MaxDepth(3)
                .ForMember(d=>d.UserRented, opt=>opt.MapFrom(src=>src.User.Name))
                .ForMember(d=>d.BookRented, opt=>opt.MapFrom(src=>src.Book.Title));
            CreateMap<UserBooks, UserBooksReferenceDTO>().PreserveReferences().MaxDepth(3)
                .ForMember(d=>d.UserId, opt=>opt.MapFrom(src=>src.UserId))
                .ForMember(d=>d.BookId, opt=>opt.MapFrom(src=>src.BookId));
        }
    }

    public class AutoMapperViewModels : Profile
    {
        public AutoMapperViewModels()
        {
            CreateMap<GenreViewModel, Genre>();
            CreateMap<UserViewModel, User>();
            CreateMap<AuthorViewModel, Author>();
            CreateMap<BookViewModel, Book>();
            CreateMap<AuthorBooksViewModel, AuthorBooks>();
            CreateMap<UserBooksViewModel, UserBooks>();
        }
    }
}