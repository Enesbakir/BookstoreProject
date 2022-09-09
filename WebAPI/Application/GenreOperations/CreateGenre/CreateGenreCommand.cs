using System;
using System.Linq;
using AutoMapper;
using WebAPI.DBoperations;
using WebAPI.Entities;

namespace WebAPI.BooksOperations.CreateBooks
{
    public class CreateGenreCommand
    {   
        public CreateGenreModel Model { get; set;}
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateGenreCommand(BookStoreDbContext _dbContext, IMapper mapper)
        {
            this._dbContext = _dbContext;
            _mapper = mapper;
        }

        public void Handle(){
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if(genre is not null){
                throw new InvalidOperationException("Genre already available");
            }
            if(_dbContext.Genres.Any(x=>x.Name.ToLower() == Model.Name.ToLower())){
                throw new InvalidOperationException("There is a genre with this Name.");
            }
            genre = _mapper.Map<Genre>(Model);
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
        }
    }
    public class CreateGenreModel{
        public string Name { get; set; }
    }
}
