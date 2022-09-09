using AutoMapper;
using WebAPI.Application.GenreOperations;
using WebAPI.BooksOperations.CreateBooks;

using WebAPI.Entities;
using WebAPI.UsersOperations.CreateUser;
using WebAPI.UsersOperations.GetUsers;

namespace BookStoreWebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<CreateBookModel,Book>();
            CreateMap<Genre,GetGenresViewModel>();
            CreateMap<User,UsersViewModel>();
            CreateMap<CreateGenreModel,Genre>();
            CreateMap<CreateUserModel,User>();
        }
    }

}