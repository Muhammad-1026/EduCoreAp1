using EduCoreApi.Application.Feature.Departments.Specifications;
using EduCoreApi.Application.Feature.Departments.Models;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Departments.Queries;

public sealed record GetDepartmentById(Guid DepartmentId) : IRequest<ApiResponse<GetDepartmentDto>>;

public sealed class GetDepartmentByIdValidator : AbstractValidator<GetDepartmentById>
{
    public GetDepartmentByIdValidator()
    {
        RuleFor(x => x.DepartmentId)
            .GreaterThan(Guid.Empty);
    }
}

internal sealed class GetByIdDepartmentHandler : IRequestHandler<GetDepartmentById, ApiResponse<GetDepartmentDto>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public GetByIdDepartmentHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<GetDepartmentDto>> Handle(GetDepartmentById request, CancellationToken cancellationToken)
    {

        var spec = new DepartmentByIdSpec(request.DepartmentId, asNoTracking: true);
        var department = await _departmentRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (department == null)
        {
            return new ApiResponse<GetDepartmentDto>
            {
                Code = 404,
                Message = "Department not found",
            };
        }

        return new ApiResponse<GetDepartmentDto>
        {
            Code = 200,
            Message = "Department retrieved successfully",
            Data = _mapper.Map<GetDepartmentDto>(department)
        };
    }
}