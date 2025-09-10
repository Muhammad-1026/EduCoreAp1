using EduCoreApi.Application.Feature.Speciality.Models;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Specialitys.Commands;

public sealed record CreateSpecialityCommand(string Name, string Code, Guid DepartmentId, string? Description) : IRequest<CreateSpecialityDto>;

public sealed class CreateSpecialityCommandValidator : AbstractValidator<CreateSpecialityCommand>
{
    public CreateSpecialityCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        RuleFor(x => x.Code)
            .NotEmpty();
        RuleFor(x => x.DepartmentId)
            .NotEmpty();
        RuleFor(x => x.Description)
            .NotEmpty();
    }
}

internal sealed class CreateSpecialityCommandHandler : IRequestHandler<CreateSpecialityCommand, CreateSpecialityDto>
{
    // Implementation of the handler would go here
    public async Task<CreateSpecialityDto> Handle(CreateSpecialityCommand request, CancellationToken cancellationToken)
    {
        var speciality = new CreateSpecialityDto
        {
            Name = request.Name,
            Code = request.Code,
            DepartmentId = request.DepartmentId,
        };

        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            speciality.SetDescription(request.Description);



        }
}