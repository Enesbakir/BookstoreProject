using System.Collections.Generic;
using System.Linq;
using WebAPI.DBoperations;
using WebAPI.Common;
using WebAPI.Entities;

namespace WebAPI.BooksOperations.GetBooks
{
    public class GetBooksQuery
    {

        private Repository<Book> _bookRepository;
        private Repository<Genre> _genreRepository;
        private readonly BookStoreDbContext _dbContext;

        public GetBooksQuery(BookStoreDbContext dbContext, Repository<Book> bookRepository)
        {
            _dbContext = dbContext;
            _bookRepository = bookRepository;
            _genreRepository= new Repository<Genre>(_dbContext);
        }

        public List<BooksViewModel> Handle(){
            var booklist= _bookRepository.GetAll().ToList();
            //var  booklist = _dbContext.Books.OrderBy(x => x.Id).ToList();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach(var book in booklist){
                vm.Add(new BooksViewModel(){
                    Title = book.Title,
                    Genre= _genreRepository.GetById(book.GenreId).Name,
                    //Genre = _dbContext.Genres.Where(x=>x.Id == book.GenreId).SingleOrDefault().Name,
                    PageCount = book.PageCount,
                });
            }
            return vm;
        }
    }

    public class BooksViewModel{
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Genre{ get; set; }
    }
}