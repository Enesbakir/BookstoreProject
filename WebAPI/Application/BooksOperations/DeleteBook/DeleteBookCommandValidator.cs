using FluentValidation;

namespace WebAPI.BooksOperations.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>{
        public DeleteBookCommandValidator(){
            RuleFor(query => query.BookId).GreaterThan(0);
        }
     }
}
