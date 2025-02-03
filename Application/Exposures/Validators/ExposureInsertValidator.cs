using Application.Authors.Validators;
using Application.Exposures.DTOs;
using FluentValidation;

namespace Application.Exposures.Validators;

public class ExposureInsertValidator : AbstractValidator<ExposureInsertDto>
{
    public ExposureInsertValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de exposición es requerido.")
            .MinimumLength(3).WithMessage("El nombre de exposición debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El nombre de exposición no debe exceder los 100 caracteres.");
        
        RuleFor(x => x.ResearchLine)
            .NotEmpty().WithMessage("La línea de investigación es requerida.");
        
        RuleFor(x => x.Type)
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