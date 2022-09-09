using FluentValidation;

namespace WebAPI.BooksOperations.DeleteBook
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>{
        public DeleteGenreCommandValidator(){
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
     }
}
