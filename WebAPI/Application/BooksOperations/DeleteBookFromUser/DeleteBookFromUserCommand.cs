using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using WebAPI.DBoperations;
using WebAPI.Entities;

namespace WebAPI.BooksOperations.DeleteBookFromUser
{
    public class DeleteBookFromUserCommand
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly HttpRequest _request;
        public DeleteBookFromUserModel Model;
        public DeleteBookFromUserCommand(BookStoreDbContext dbContext,HttpRequest request)
        {
            _dbContext = dbContext;
            _request = request;
        }
        public void Handle(){
            int UserId= (int)_request.HttpContext.Session.GetInt32("UserId"); 
            var book = _dbContext.UserBooks.FirstOrDefault(x => x.BookId == Model.BookId && x.UserId==UserId);
            if(book is null){
                throw new InvalidOperationException("Book id isn't available");
            }
            var user =_dbContext.Users.SingleOrDefault(x => x.Id == UserId);
            _dbContext.UserBooks.Remove(book);   
            _dbContext.SaveChanges();
        }
    }
    public class DeleteBookFromUserModel{
        public int BookId { get; set; }
    }
}