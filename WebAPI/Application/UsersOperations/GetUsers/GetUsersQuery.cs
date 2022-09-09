using System.Collections.Generic;
using System.Linq;
using WebAPI.DBoperations;
using WebAPI.Common;
using WebAPI.Entities;
using AutoMapper;
using System;

namespace WebAPI.UsersOperations.GetUsers
{
    public class GetUsersQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetUsersQuery(BookStoreDbContext dbContext,IMapper mapper){
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public List<UsersViewModel> Handle(){
            var  usersList = _dbContext.Users.OrderBy(x => x.Name).ToList();
            List<UsersViewModel> vm = _mapper.Map <List<UsersViewModel>>(usersList);
            return vm;
        }
    }

    public class UsersViewModel{
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email{ get; set; }
        public string Password { get; set; }
        public string RefreshToken  {get;set;}
    }
}