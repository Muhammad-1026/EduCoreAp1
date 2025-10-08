using EduCoreApi.Application.Feature.Departments.Models;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Domain.Models;
using Ardalis.Specification;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Departments.Queries;

public sealed record GetDepartments : IRequest<ApiResponse<List<GetDepartmentDto>>>;

internal sealed class GetDepartmentsHandler(IDepartmentRepository departmentRepository, IMapper mapper) : IRequestHandler<GetDepartments, ApiResponse<List<GetDepartmentDto>>>
{
    public async Task<ApiResponse<List<GetDepartmentDto>>> Handle(GetDepartments request, CancellationToken cancellationToken)
    {
        var spec = new DbSpecifications<Department>();
        spec.Query.AsNoTracking();

        var departments = await departmentRepository.ListAsync(spec, cancellationToken);

        if (departments == null)
        {
            return new ApiResponse<List<GetDepartmentDto>>
            {
                Code = 404,
                Message = "Departments no found"
            };
        }

        var departmentDto = mapper.Map<List<GetDepartmentDto>>(departments);

        return new ApiResponse<List<GetDepartmentDto>>
        {
            Code = 200,
            Message = "Departments retrieved successfully",
            Data = departmentDto
        };
    }
}