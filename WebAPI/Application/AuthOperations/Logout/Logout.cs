using System;
using System.Linq;
using WebAPI.DBoperations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using WebAPI.Entities;

namespace WebAPI.Application.AuthOperations.LoginOperation
{
    public class Logout{
        public LoginUserModel Model { get; set;}
        private readonly BookStoreDbContext _dbContext;    
        private readonly HttpRequest _request; 
        public Logout(BookStoreDbContext _dbContext,HttpRequest request){
            this._dbContext = _dbContext;
            _request = request;
        }
        public void Handle(){
            var userId=_request.HttpContext.Session.GetInt32("UserId");
            var user = _dbContext.Users.SingleOrDefault(x => x.Id == userId);
            if(user is  null){
                throw new InvalidOperationException("userıd Problem");
            }
            user.RefreshTokenExpiredDate= DateTime.Now;
            _request.HttpContext.Session.Clear();
        }
    }

}