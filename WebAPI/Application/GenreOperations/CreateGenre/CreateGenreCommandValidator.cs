using FluentValidation;

namespace WebAPI.BooksOperations.CreateBooks
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>{
        public CreateGenreCommandValidator(){
            RuleFor(query => query.Model.Name).NotEmpty().MinimumLength(4);
        }
     }
}
