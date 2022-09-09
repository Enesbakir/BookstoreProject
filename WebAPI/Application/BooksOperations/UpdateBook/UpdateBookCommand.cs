using System;
using System.Linq;
using WebAPI.DBoperations;

namespace WebAPI.BooksOperations.UpdateBooks 
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model{ get; set;}
        private readonly BookStoreDbContext _dbContext;
        
        public int BookId { get; set; }
        public UpdateBookCommand(BookStoreDbContext _dbContext){
            this._dbContext = _dbContext;
        }
        public void Handle(){
            var book =_dbContext.Books.Where(book=> book.Id == BookId).SingleOrDefault();
            if(book is null){
                throw new InvalidOperationException("There isn't a book with this Id.");
            }
            if(_dbContext.Genres.SingleOrDefault(x => x.Id == Model.GenreId) is null){
                throw new InvalidOperationException("Id isn't available");
            }
            if(_dbContext.Books.SingleOrDefault(x => x.Title.ToLower() == Model.Title.ToLower() && x.Id != BookId) is not null){
                 throw new InvalidOperationException("Book already available");
            }
            book.GenreId =Model.GenreId !=default ? Model.GenreId:book.GenreId;
            book.PageCount =Model.PageCount!=default ? Model.PageCount:book.PageCount;
            book.Title =Model.Title !=default ? Model.Title:book.Title;
            _dbContext.SaveChanges();
        }
    }
    public class UpdateBookModel{
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount {get; set; }
    }
}