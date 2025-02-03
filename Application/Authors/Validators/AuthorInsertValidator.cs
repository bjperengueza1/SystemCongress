using Application.Authors.DTOs;
using FluentValidation;

namespace Application.Authors.Validators;

public class AuthorInsertValidator : AbstractValidator<AuthorInsertDto>
{
    public AuthorInsertValidator()
    {
        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("La posición del autor es requerida.")
            .GreaterThan(0)
            .LessThan(4);
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del autor es requerido.")
            .MinimumLength(10).WithMessage("El nombre del autor debe tener al menos 10 caracteres.")
            .MaximumLength(100).WithMessage("El nombre del autor no debe exceder los 100 caracteres.");
        
        RuleFor(x => x.IDNumber)
            .NotEmpty().WithMessage("El número de identificación es requerido.")
            .MinimumLength(10).WithMessage("El número de identificación debe tener al menos 10 caracteres.")
            .MaximumLength(20);
        
        RuleFor(x => x.InstitutionalMail)
            .NotEmpty().WithMessage("El correo institucional es requerido.")
            .MaximumLength(100).WithMessage("El correo institucional no debe exceder los 100 caracteres.");
        
        RuleFor(x => x.PersonalMail)
            .NotEmpty().WithMessage("El correo personal es requerido.")
            .MaximumLength(100).WithMessage("El correo personal no debe exceder los 100 caracteres.");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("El número de teléfono es requerido.")
            .MaximumLength(20).WithMessage("El número de teléfono no debe exceder los 20 caracteres.");
        
        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("El país es requerido.")
            .MinimumLength(3).WithMessage("El país debe tener al menos 3 caracteres.")
            .MaximumLength(20).WithMessage("El país no debe exceder los 20 caracteres.");
        
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("La ciudad es requerida.")
            .MinimumLength(3).WithMessage("La ciudad debe tener al menos 3 caracteres.")
            .MaximumLength(20).WithMessage("La ciudad no debe exceder los 20 caracteres.");
        
        RuleFor(x => x.AcademicDegree)
            .NotEmpty().WithMessage("El grado académico es requerido.");
    }
    
}