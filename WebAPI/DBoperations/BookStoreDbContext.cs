using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.DBoperations
{
    public class BookStoreDbContext: DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options): base(options)
        {}
            public DbSet <Book> Books { get; set; }
            public DbSet <Genre> Genres {get;set;}
            public DbSet <User> Users {get;set;}
            public DbSet <UserBooks> UserBooks {get;set;}
    }
}