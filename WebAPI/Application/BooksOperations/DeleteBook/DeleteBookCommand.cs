using System;
using System.Linq;
using WebAPI.DBoperations;

namespace WebAPI.BooksOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext _dbContext){
            this._dbContext = _dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id==BookId);
            if(book is null){
                throw new InvalidOperationException("There isn't a book with this Id.");
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }

}