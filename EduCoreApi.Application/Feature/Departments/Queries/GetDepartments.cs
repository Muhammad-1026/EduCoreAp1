using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Departments.Models;
using EduCoreApi.Application.Feature.Departments.Repositories;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Departments.Queries;

public sealed record GetDepartments : IRequest<ApiResponse<List<GetDepartmentDto>>>;

interface sealed class GetDepartmentsHandler : IRequestHandler<GetDepartments, ApiResponse<List<GetDepartmentDto>>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public GetDepartmentsHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public Task<ApiResponse<List<GetDepartmentDto>>> Handle(GetDepartments request, CancellationToken cancellationToken)
    {
        try
        {

        }
        catch (Exception error)
        {
            return new ApiResponse<List<GetDepartmentDto>>
            {
                Code = 0,
                Message = $"Error: {error.Message}",
                Data = null
            };
        }    
    }
} 