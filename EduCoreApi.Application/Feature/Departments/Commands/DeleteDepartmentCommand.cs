using EduCoreApi.Application.Feature.Departments.Specifications;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Departments.Commands;

public sealed record DeleteDepartmentCommand(Guid DepartmentId) : IRequest<ApiResponse>;

public sealed class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
{
    public DeleteDepartmentCommandValidator()
    {
        RuleFor(x => x.DepartmentId)
            .GreaterThan(Guid.Empty);
    }
}

internal sealed class DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository) : IRequestHandler<DeleteDepartmentCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await departmentRepository.FirstOrDefaultAsync(new DepartmentByIdSpec(request.DepartmentId), cancellationToken);

        if (department is null)
        {
            return new ApiResponse
            {
                Code = 404,
                Message = "Department not found"
            };
        }

        await departmentRepository.DeleteAsync(department, cancellationToken);
        await departmentRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse
        {
            Code = 200,
            Message = "Department deleted successfully",
        };
    }
}