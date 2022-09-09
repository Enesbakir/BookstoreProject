using FluentValidation;
using WebAPI.BooksOperations.UpdateBooks;

namespace WebAPI.BooksOperations.UpdatedBooks
{
     public class UpdateGenreCommandValidator: AbstractValidator<UpdateGenreCommand>{
        public UpdateGenreCommandValidator(){
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
     }
}