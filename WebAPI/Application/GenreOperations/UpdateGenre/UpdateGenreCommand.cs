using System;
using System.Linq;
using WebAPI.DBoperations;

namespace WebAPI.BooksOperations.UpdateBooks
{
    public class UpdateGenreCommand
    {
        public UpdateGenreModel Model{ get; set;}
        private readonly BookStoreDbContext _dbContext;

        public int GenreId { get; set; }
        public UpdateGenreCommand(BookStoreDbContext _dbContext){
            this._dbContext = _dbContext;
        }
        public void Handle(){
            var genre =_dbContext.Genres.Where(genre=> genre.Id == GenreId).SingleOrDefault();
            if(genre is null){
                throw new InvalidOperationException("There isn't a genre with this Id.");
            }
            if(Model.Name is not null && _dbContext.Genres.Any(x=>x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId)){
                throw new InvalidOperationException("There is a genre with this Name.");
            }
            genre.Name =Model.Name !=default ? Model.Name:genre.Name;
            genre.IsActive= Model.IsActive;
            _dbContext.SaveChanges();
        }
    }
    public class UpdateGenreModel{
        public string Name { get; set; }
        public bool IsActive { get; set; }=true;
    }
}