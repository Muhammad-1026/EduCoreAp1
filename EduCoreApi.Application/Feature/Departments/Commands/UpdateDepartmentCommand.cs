using EduCoreApi.Application.Feature.Departments.Specifications;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Departments.Commands;

public sealed record UpdateDepartmentCommand(Guid DepartmentId,string Name,string? Description) : IRequest<ApiResponse<Guid>>;

public sealed class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{ 
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(x => x.DepartmentId)
            .GreaterThan(Guid.Empty);

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty();
    }
}

internal sealed class UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository) : IRequestHandler<UpdateDepartmentCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await departmentRepository.FirstOrDefaultAsync(new DepartmentByIdSpec(request.DepartmentId), cancellationToken);

        if (department is null)
        {
            return new ApiResponse<Guid>
            {
                Code = 404,
                Message = "Department not found"
            };
        }

        department.SetName(request.Name);

        if (!string.IsNullOrWhiteSpace(request.Description))
            department.SetDescription(request.Description);

        await departmentRepository.UpdateAsync(department, cancellationToken);
        await departmentRepository.SaveChangesAsync(cancellationToken);


        return new ApiResponse<Guid>
        {
            Code = 200,
            Data = department.Id,
            Message = "Department updated successfully"
        };
    }
}