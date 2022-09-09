using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.TokenOperations.Models;
using WebAPI.Entities;

namespace WebAPI.TokenOperations
{
    public class TokenHandler{
        public IConfiguration Configuration{get;set;}
        public TokenHandler(IConfiguration configuration){
            Configuration =configuration;
        }

        public Token CreateAccessToken(User user){
            Token tokenModel=new Token();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            tokenModel.Expiration =DateTime.Now.AddMinutes(1);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer:Configuration["Token:Issuer"],
                audience:Configuration["Token:Audience"],
                expires:tokenModel.Expiration,
                notBefore: DateTime.Now,
                signingCredentials : credentials
            );
            JwtSecurityTokenHandler tokenHandler =new JwtSecurityTokenHandler();
            tokenModel.AccessToken=tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken=CreateRefreshToken();
            return tokenModel;
        }
        public string CreateRefreshToken(){
            return Guid.NewGuid().ToString();
        }
    }
}