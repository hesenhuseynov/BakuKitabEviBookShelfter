using BookShelfter.Application.Features.Commands.Book.CreateBook;
using BookShelfter.Application.ViewModels.Books;
using FluentValidation;

namespace BookShelfter.Application.Validators.Books;

public class CreateBookValidator:AbstractValidator<CreateBookCommandRequest>
{
    public CreateBookValidator()
    {
        RuleFor(p => p.BookName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter the name of book")
            .MaximumLength(150)
            .MinimumLength(5)
            .WithMessage("Please Enter only between minimum 5- maximum 150 character name of book");

        RuleFor(p => p.Stock)
            .NotEmpty()
            .NotNull()
            .WithMessage(" Stock should not be empty")
            .Must(s => s >= 0)
            .WithMessage("Stock should not be negative ");

        RuleFor(p => p.Price)
            .NotEmpty()
            .NotNull()
            .WithMessage(" please enter the stock  ")
            .Must(s => s >= 0)
            .WithMessage(" stock should not be negative");

        
        
    
        


    }
    
}