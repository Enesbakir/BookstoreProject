using System;
using System.Net.Http;
using System.Net.Http.Headers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.TokenOperations.Models;
using WebAPI.DBoperations;
using WebAPI.UsersOperations.CreateToken;
using WebAPI.UsersOperations.CreateUser;
using WebAPI.UsersOperations.GetUsers;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController:ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;

        public UserController(BookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult getBook(){
            GetUsersQuery query = new GetUsersQuery(_context,_mapper);
            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser){
            CreateUserCommand command = new CreateUserCommand(_context,_mapper);
            command.Model= newUser;
            command.Handle();
            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login){
            CreateTokenCommand command = new CreateTokenCommand(_context,_mapper,_configuration);
            command.Model =login;
            Token token = command.Handle();
            // HttpContext.Session.SetString("JWToken", token.AccessToken);
            return token;
        }
    }
}