using FluentValidation;
using WebAPI.BooksOperations.UpdateBooks;

namespace WebAPI.BooksOperations.UpdatedBooks
{
     public class UpdateBookCommandValidator: AbstractValidator<UpdateBookCommand>{
        public UpdateBookCommandValidator(){
            RuleFor(query => query.BookId).GreaterThan(0);
            RuleFor(query => query.Model.GenreId).GreaterThan(0);
            RuleFor(query => query.Model.Title).NotEmpty().MinimumLength(4);
        }
     }

}