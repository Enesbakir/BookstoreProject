using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Entities;

namespace WebAPI.DBoperations{

    public class DataGenerator{
        public static void Initialize(IServiceProvider serviceProvider){
            // using (var context = new BookStoreDbContext(
            //     serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>())){
            //         if (context.Books.Any())
            //         {
            //             return;   // Data was already seeded
            //         }

            //         context.Genres.AddRange(
            //             new Genre{
            //                 Name ="Personal Growth",             
            //             },
            //             new Genre{
            //                 Name ="Comedy",             
            //             },
            //             new Genre{
            //                 Name ="Action",             
            //             }
            //         );

            //         context.Books.AddRange(
            //             new Book{
            //                 GenreId =1,
            //                 PageCount =550,
            //                 Title = "İstanbul Hatırası"
            //             },
            //             new Book{
            //                 GenreId =3,
            //                 PageCount =300,
            //                 Title = "Harry Potter Philosopher's Stone"
            //             },
            //             new Book{
            //                 GenreId =3,
            //                 PageCount =450,
            //                 Title = "Harry Potter and Deathly Hallows"
            //             });
            //         context.SaveChanges();
            // }
        }
    }
}