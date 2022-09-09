using System.Collections.Generic;
using System.Linq;
using WebAPI.DBoperations;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Http;

namespace WebAPI.BooksOperations.GetUserBooks
{
    public class GetUserBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
         private readonly HttpRequest _request;
        private readonly IMapper _mapper;
        public GetUserBooksQuery(BookStoreDbContext dbContext,IMapper mapper,HttpRequest request){
            _dbContext = dbContext;
            _mapper = mapper;
             _request = request;
        }
        
        public List<UsersBooksViewModel> Handle(){      
            int UserId= (int)_request.HttpContext.Session.GetInt32("UserId");     
            var booksIds =_dbContext.UserBooks.Where(x=> x.UserId == UserId);
            List<UsersBooksViewModel> vm = new List<UsersBooksViewModel>();
            foreach(var bookId in booksIds.ToList()){
                var book = _dbContext.Books.Where(x=> x.Id == bookId.BookId).SingleOrDefault();
                vm.Add(new UsersBooksViewModel(){
                    Title =book.Title
                });
            }
            return vm;
        }
    }

    public class UsersBooksViewModel{
        public string Title { get; set; }
        
    }
}