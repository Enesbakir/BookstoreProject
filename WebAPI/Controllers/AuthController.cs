using System;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.AuthOperations.LoginOperation;
using WebAPI.DBoperations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AuthController:ControllerBase
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthController(BookStoreDbContext dbContext, IMapper mapper){
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody ] LoginUserModel user){////////*UserIdyi almaya çalış session kurmaya çalış///
            Login query = new Login(_dbContext,Request);
            query.Model=user;
            query.Handle();
            return Ok();
        }

        [HttpGet("logout")]
        public IActionResult Logout(){////////*UserIdyi almaya çalış session kurmaya çalış///
            Logout query = new Logout(_dbContext,Request);
            query.Handle();
            return Ok();
        }
    }

}
