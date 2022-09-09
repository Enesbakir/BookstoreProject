using System;
using System.Linq;
using WebAPI.DBoperations;

namespace WebAPI.BooksOperations.DeleteBook
{
    public class DeleteGenreCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int GenreId { get; set; }
        public DeleteGenreCommand(BookStoreDbContext _dbContext){
            this._dbContext = _dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id==GenreId);
            if(genre is null){
                throw new InvalidOperationException("There isn't a genre with this Id.");
            }
            if(_dbContext.Books.FirstOrDefault(x => x.GenreId==genre.Id) is not null){
                throw new InvalidOperationException("Genre can't be deleted.");
            }

            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
        }
    }

}