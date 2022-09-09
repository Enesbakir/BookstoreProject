using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.TokenOperations.Models;
using WebAPI.DBoperations;
using System.Web;
using WebAPI.TokenOperations;

namespace WebAPI.UsersOperations.CreateToken
{
    public class RefreshTokenCommand
    {   
        private readonly BookStoreDbContext _dbContext;
        private String RefreshToken {get;set;}
        private readonly IConfiguration _configuration;
        public RefreshTokenCommand(BookStoreDbContext _dbContext, IConfiguration configuration)
        {
            this._dbContext = _dbContext;
            _configuration = configuration;
        }

        public Token Handle(){
            var user = _dbContext.Users.SingleOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpiredDate > DateTime.Now);
            if(user is null){
                throw new InvalidOperationException("Invalid Refresh Token");
            }else
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(user); 
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpiredDate= token.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;
            }
        }
    }
}
