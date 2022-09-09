using FluentValidation;

namespace WebAPI.BooksOperations.CreateBooks
{
    public class CreateBooksCommandValidator : AbstractValidator<CreateBooksCommand>{
        public CreateBooksCommandValidator(){
            RuleFor(query => query.Model.PageCount).GreaterThan(0);
            RuleFor(query => query.Model.GenreId).GreaterThan(0);
            RuleFor(query => query.Model.Title).NotEmpty().MinimumLength(4);
        }
     }
}
