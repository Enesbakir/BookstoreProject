using System;
using System.Linq;
using AutoMapper;
using WebAPI.DBoperations;
using WebAPI.Entities;

namespace WebAPI.BooksOperations.CreateBooks
{
    public class CreateBooksCommand
    {   
        public CreateBookModel Model { get; set;}
        private Repository<Book> _bookRepository;
        private Repository<Genre> _genreRepository;
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBooksCommand(BookStoreDbContext _dbContext, IMapper mapper,Repository<Book> bookRepository)
        {
            this._dbContext = _dbContext;
            _mapper = mapper;
             _bookRepository = bookRepository;
             _genreRepository = new Repository<Genre>(_dbContext);
        }

        public void Handle(){
            var book = _bookRepository.Get(x => x.Title == Model.Title);
            //var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if(book is not null){
                throw new InvalidOperationException("Book already available");
            }
            //_dbContext.Genres.SingleOrDefault(x => x.Id == Model.GenreId)
            if(_genreRepository.GetById(Model.GenreId) is null){
                throw new InvalidOperationException("Genre Id isn't available");
            }
            book = _mapper.Map<Book>(Model);
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
    }


    public class CreateBookModel{
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount {get; set; }
    }
}
