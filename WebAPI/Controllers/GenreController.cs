using System.Net.Http;
using System.Net.Http.Headers;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.GenreOperations;
using WebAPI.BooksOperations.CreateBooks;
using WebAPI.BooksOperations.DeleteBook;
using WebAPI.BooksOperations.GetBookDetails;
using WebAPI.BooksOperations.UpdateBooks;
using WebAPI.BooksOperations.UpdatedBooks;
using WebAPI.DBoperations;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class GenreController:ControllerBase
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GenreController(BookStoreDbContext dbContext, IMapper mapper)

        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult getGenre(){
            GetGenresQuery query = new GetGenresQuery(_dbContext,_mapper);
            // var token = HttpContext.Session.GetString("JWToken");
            // var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/Genres");
            // request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return Ok(query.Handle());
        }
        [HttpPost]
        public IActionResult addBook([FromBody] CreateGenreModel newBook){
            CreateGenreCommand command = new CreateGenreCommand(_dbContext,_mapper);
            command.Model=newBook;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
            //return Created("",command);
        }

        [HttpDelete("{id}")]
        public IActionResult deleteGenre(int id){
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            command.GenreId=id;
            DeleteGenreCommandValidator validator =new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        } 
        
        [HttpPut("{id}")]
        public IActionResult updateGenre(int id,[FromBody] UpdateGenreModel updateGenre){
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            command.GenreId=id;
            command.Model=updateGenre;
            UpdateGenreCommandValidator validator =new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

         [HttpGet("{id}")]
        public IActionResult getGenreById(int id){
            GetGenreDetailsQuery query = new GetGenreDetailsQuery(_dbContext);
            query.GenreId = id;
            GetGenreDetailsQueryValidator validator = new GetGenreDetailsQueryValidator();
            validator.ValidateAndThrow(query);
            return Ok(query.Handle());
        }
    }


}
