using Application.Authors.Validators;
using Application.Exposures.DTOs;
using FluentValidation;

namespace Application.Exposures.Validators;

public class ExposureInsertValidator : AbstractValidator<ExposureInsertDto>
{
    public ExposureInsertValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
        
        RuleFor(x => x.ResearchLine)
            .NotEmpty();
        
        RuleFor(x => x.CongressId)
            .NotEmpty();
        
        RuleFor(x => x.Authors)
            .NotEmpty();
        
        RuleForEach(x => x.Authors)
            .SetValidator(new AuthorInsertValidator());
        
        RuleFor(x => x.SummaryFilePath)
            .NotEmpty();
    }
    
}