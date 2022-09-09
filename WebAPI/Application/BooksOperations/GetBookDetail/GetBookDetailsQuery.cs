using System;
using System.Linq;
using WebAPI.Common;
using WebAPI.DBoperations;
using WebAPI.Entities;

namespace WebAPI.BooksOperations.GetBookDetails
{
    public class GetBookDetailsQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private Repository<Book> _bookRepository;
        private Repository<Genre> _genreRepository;
        public int BookId { get; set; }
        public GetBookDetailsQuery(BookStoreDbContext dbContext,Repository<Book> bookRepository)
        {
            _dbContext = dbContext;
            _bookRepository= bookRepository;
            _genreRepository= new Repository<Genre>(_dbContext);

        }

        public BookDetailModel Handle(){
            var book = _bookRepository.GetById(BookId);
            //var book =_dbContext.Books.Where(book=> book.Id == BookId).SingleOrDefault();
            BookDetailModel vm = new BookDetailModel();
            if(book is null){
                throw new InvalidOperationException("There isn't a book with this Id.");
            }
            vm.Title=book.Title;
            //vm.Genre=((GenreEnum)(book.GenreId)).ToString();
            vm.Genre = _genreRepository.GetById(book.GenreId).Name;
            //vm.Genre = _dbContext.Genres.Where(x=>x.Id == book.GenreId).SingleOrDefault().Name;
            vm.PageCount=book.PageCount;
            return vm;
        } 
    }
    public class BookDetailModel{
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount {get; set; }
    }
}