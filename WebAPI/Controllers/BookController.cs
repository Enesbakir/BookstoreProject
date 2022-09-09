using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BooksOperations.AddBookToUser;
using WebAPI.BooksOperations.CreateBooks;
using WebAPI.BooksOperations.DeleteBook;
using WebAPI.BooksOperations.DeleteBookFromUser;
using WebAPI.BooksOperations.GetBookDetails;
using WebAPI.BooksOperations.GetBooks;
using WebAPI.BooksOperations.GetUserBooks;
using WebAPI.BooksOperations.UpdateBooks;
using WebAPI.BooksOperations.UpdatedBooks;
using WebAPI.DBoperations;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private Repository<Book> _bookRepository;
        private readonly BookStoreDbContext _dbContext ;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _bookRepository = new Repository<Book>(_dbContext);
        }
        [HttpGet]
        public IActionResult getUserBooks(){////////*UserIdyi almaya çalış session kurmaya çalış///
            GetUserBooksQuery query = new GetUserBooksQuery(_dbContext,_mapper,Request);
            return Ok(query.Handle());
        }

         [HttpGet("all")]
        public IActionResult getBook(){
            GetBooksQuery query = new GetBooksQuery(_dbContext,_bookRepository);
            return Ok(query.Handle());
        }
       
        [HttpGet("{id}")]
        public IActionResult getBookById(int id){
            
            GetBookDetailsQuery query = new GetBookDetailsQuery(_dbContext,_bookRepository);
            query.BookId=id;
            GetBookDetailsQueryValidator validator = new GetBookDetailsQueryValidator();
            validator.ValidateAndThrow(query);
            return Ok(query.Handle());
        }
      
        [HttpPost]
        public IActionResult createBook([FromBody] CreateBookModel newBook){
            CreateBooksCommand command = new CreateBooksCommand(_dbContext,_mapper,_bookRepository);
            newBook.Title=newBook.Title.Trim();
            command.Model=newBook;
            CreateBooksCommandValidator validator = new CreateBooksCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
            //return Created("",command);
        }
        [HttpPost("add")]
        public IActionResult addBookToUser([FromBody] AddBookToUserModel model){
            AddBookToUserCommand command =new AddBookToUserCommand(_dbContext,_mapper);
            command.UserId= (int)(Request.HttpContext.Session.GetInt32("UserId"));
            command.Model= model;
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult updateBook(int id,[FromBody] UpdateBookModel updateBook){
            UpdateBookCommand command = new UpdateBookCommand(_dbContext);
            command.BookId=id;
            updateBook.Title=updateBook.Title.Trim();
            command.Model=updateBook;
            UpdateBookCommandValidator validator =new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
       
        [HttpDelete("{id}")]
        public IActionResult deleteBook(int id){
            DeleteBookCommand command = new DeleteBookCommand(_dbContext);
            command.BookId=id;
            DeleteBookCommandValidator validator =new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        } 

        [HttpDelete]
        public IActionResult deleteBookFromUser([FromBody] DeleteBookFromUserModel model){
            DeleteBookFromUserCommand command = new DeleteBookFromUserCommand(_dbContext,Request);
            command.Model=model;
            command.Handle();
            return Ok();
        }
    }
}