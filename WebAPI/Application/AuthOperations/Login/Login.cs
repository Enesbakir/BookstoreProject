using System;
using System.Linq;
using WebAPI.DBoperations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using WebAPI.Entities;
using WebAPI.Middlewares;

namespace WebAPI.Application.AuthOperations.LoginOperation
{
    public class Login{
        public LoginUserModel Model { get; set;}
        private readonly BookStoreDbContext _dbContext;    
        private readonly HttpRequest _request;
        public Login(BookStoreDbContext _dbContext,HttpRequest request){
            this._dbContext = _dbContext;
            _request = request;
        }
        public void Handle(){
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if(user is null){
                throw new InvalidOperationException("Wrong email or Password");
            }
            if(user.Password!=Model.Password){
                throw new InvalidOperationException("Wrong Email or Password");
            }
            _request.HttpContext.Session.SetInt32("UserId",user.Id);
        }
    }

    public class LoginUserModel{
        public string Email {get; set; }
        public string Password {get; set; }
    }
}