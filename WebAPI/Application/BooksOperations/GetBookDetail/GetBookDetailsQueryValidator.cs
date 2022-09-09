using FluentValidation;

namespace WebAPI.BooksOperations.GetBookDetails
{
     public class GetBookDetailsQueryValidator: AbstractValidator<GetBookDetailsQuery>{
        public GetBookDetailsQueryValidator(){
            RuleFor(query => query.BookId).GreaterThan(0);
        }
     }

}