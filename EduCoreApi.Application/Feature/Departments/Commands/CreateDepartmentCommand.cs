using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Feature.Departments.Models;
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

internal sealed class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, CreateDepartmentDto>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;
    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }
    public async Task<CreateDepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Department(
            name: request.Name,
            description: request.Description,
            createdBy: Guid.Empty
        );

        await _departmentRepository.AddAsync(department, cancellationToken);
        await _departmentRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CreateDepartmentDto>(department);
    }
}