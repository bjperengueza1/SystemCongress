using Application.Congresses.DTOs;
using FluentValidation;

namespace Application.Congresses.Validators;

public class CongressUpdateValidator : AbstractValidator<CongressUpdateDto>
{
    public CongressUpdateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nombre no puede ser vacío");
        RuleFor(x => x.Name).Length(2, 100).WithMessage("El nombre debe tener entre 2 y 100 caracteres.");
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("La fecha de inicio no puede estar vacía");
        RuleFor(x => x.EndDate).NotEmpty().WithMessage("La fecha de finalización no puede estar vacía");
        RuleFor(x => x.StartDate).LessThan(x => x.EndDate).WithMessage("La fecha de inicio debe ser menor que la fecha de finalización");
        RuleFor(x => x.Location).NotEmpty().WithMessage("La ubicación no puede estar vacía");
        RuleFor(x => x.MinHours).GreaterThan(0).WithMessage("Las horas mínimas deben ser mayores que 0");
    }
}