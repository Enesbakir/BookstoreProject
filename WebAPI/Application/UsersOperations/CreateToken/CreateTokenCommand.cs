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
    public class CreateTokenCommand
    {   
        public CreateTokenModel Model { get; set;}
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CreateTokenCommand(BookStoreDbContext _dbContext, IMapper mapper, IConfiguration configuration)
        {
            this._dbContext = _dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle(){
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if(user is null){
                throw new InvalidOperationException("Email or password is not correct");
            }else
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(user); 
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpiredDate= token.Expiration.AddMinutes(15);
                _dbContext.SaveChanges();
                return token;
            }
        }
    }
    public class CreateTokenModel{
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
