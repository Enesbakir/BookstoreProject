using System;
using System.Linq;
using AutoMapper;
using WebAPI.DBoperations;
using WebAPI.Entities;

namespace WebAPI.UsersOperations.CreateUser
{
    public class CreateUserCommand
    {   
        public CreateUserModel Model { get; set;}
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateUserCommand(BookStoreDbContext _dbContext, IMapper mapper)
        {
            this._dbContext = _dbContext;
            _mapper = mapper;
        }

        public void Handle(){
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == Model.Email);
            if(user is not null){
                throw new InvalidOperationException("Email already taken");
            }
            
            user = _mapper.Map<User>(Model);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }


    public class CreateUserModel{
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email {get; set; }
        public string Password {get; set; }
    }
}
