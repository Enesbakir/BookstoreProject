using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebAPI.DBoperations;
using WebAPI.Entities;

namespace WebAPI.Application.GenreOperations
{
    public class GetGenresQuery
    {
        public GetGenresViewModel  Model { get; set;}
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<GetGenresViewModel> Handle(){
            var  genreList = _dbContext.Genres.Where(x=>x.IsActive).OrderBy(x => x.Id).ToList();
            List<GetGenresViewModel> vm = _mapper.Map <List<GetGenresViewModel>>(genreList);
            return vm;
        }
    }

    public class GetGenresViewModel{
        public int Id { get; set; }
        public string Name { get; set; }
    }
}