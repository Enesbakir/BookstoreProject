using System;
using System.Linq;
using AutoMapper;
using WebAPI.DBoperations;
using WebAPI.Entities;

namespace WebAPI.BooksOperations.AddBookToUser
{
    public class AddBookToUserCommand
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int UserId;
        public AddBookToUserModel Model;
        public AddBookToUserCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle(){
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == Model.BookId);
            if(book is null){
                throw new InvalidOperationException("Book Problem");
            }
            var user =_dbContext.Users.SingleOrDefault(x => x.Id == UserId);
            if(user is null){
                throw new InvalidOperationException("User Problem");
            };
            var entry =_dbContext.UserBooks.SingleOrDefault(x => x.UserId == UserId && x.BookId==Model.BookId);
            if(entry is not null){
                throw new InvalidOperationException("Book has already added to User");
            };
            _dbContext.UserBooks.Add(new UserBooks{UserId=UserId,BookId=Model.BookId});   
            _dbContext.SaveChanges();
        }
    }
    public class AddBookToUserModel{
        public int BookId { get; set; }
    }
}