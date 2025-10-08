using EduCoreApi.Application.Feature.Departments.Models;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Domain.Models;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Departments.Commands;

public sealed record CreateDepartmentCommand(string Name, string? Description) : IRequest<CreateDepartmentDto>;

public sealed class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty()
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}

internal sealed class CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper) : IRequestHandler<CreateDepartmentCommand, CreateDepartmentDto>
{
    public async Task<CreateDepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Department(
            name: request.Name,
            description: request.Description,
            createdBy: Guid.Empty
        );

        await departmentRepository.AddAsync(department, cancellationToken);
        await departmentRepository.SaveChangesAsync(cancellationToken);

        return mapper.Map<CreateDepartmentDto>(department);
    }
}