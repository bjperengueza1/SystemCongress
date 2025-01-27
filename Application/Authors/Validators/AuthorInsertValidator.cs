using Application.Authors.DTOs;
using FluentValidation;

namespace Application.Authors.Validators;

public class AuthorInsertValidator : AbstractValidator<AuthorInsertDto>
{
    public AuthorInsertValidator()
    {
        RuleFor(x => x.Position)
            .NotEmpty()
            .GreaterThan(0)
            .LessThan(4);
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(100);
        
        RuleFor(x => x.IDNumber)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(20);
        
        RuleFor(x => x.InstitutionalMail)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.PersonalMail)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(20);
        
        RuleFor(x => x.Country)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(20);
        
        RuleFor(x => x.City)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(20);
        
        RuleFor(x => x.AcademicDegree)
            .NotEmpty();
    }
    
}