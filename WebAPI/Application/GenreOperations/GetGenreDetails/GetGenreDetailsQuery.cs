using System;
using System.Linq;
using WebAPI.Common;
using WebAPI.DBoperations;

namespace WebAPI.BooksOperations.GetBookDetails
{
    public class GetGenreDetailsQuery
    {
        private readonly BookStoreDbContext _dbContext;

        public int GenreId { get; set; }
        public GetGenreDetailsQuery(BookStoreDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public GenreDetailModel Handle(){
            var genre =_dbContext.Genres.Where(genre=> genre.Id == GenreId && genre.IsActive).SingleOrDefault();
            GenreDetailModel vm = new GenreDetailModel();
            if(genre is null){
                throw new InvalidOperationException("There isn't a genre with this Id.");
            }
            vm.Name=genre.Name;
            vm.Id =genre.Id;
            return vm;
        } 
    }

    public class GenreDetailModel{
        public string Name { get; set; }
        public int Id { get; set; }
    }
}