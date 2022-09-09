using FluentValidation;

namespace WebAPI.BooksOperations.GetBookDetails
{
     public class GetGenreDetailsQueryValidator: AbstractValidator<GetGenreDetailsQuery>{
        public GetGenreDetailsQueryValidator(){
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
     }

}