using EduCoreApi.Application.Feature.Teachers.Repositories;
using EduCoreApi.Application.Feature.Teachers.Models;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Domain.Models;
using Ardalis.Specification;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Teachers.Queries;

public sealed record GetTeachers : IRequest<ApiResponse<List<GetTeacherDto>>>;

internal sealed class GetTeachersHandler(ITeacherRepository teacherRepository, IMapper mapper)
    : IRequestHandler<GetTeachers, ApiResponse<List<GetTeacherDto>>>
{
    public async Task<ApiResponse<List<GetTeacherDto>>> Handle(GetTeachers request, CancellationToken cancellationToken)
    {
        var spec = new DbSpecifications<Teacher>();
        spec.Query.Where(t => t.IsActive);
        spec.Query.AsNoTracking();
        spec.Query.Include(t => t.Department);

        var teachers = await teacherRepository.ListAsync(spec, cancellationToken);

        var teacherDto = mapper.Map<List<GetTeacherDto>>(teachers);

        return new ApiResponse<List<GetTeacherDto>>
        {
            Code = 200,
            Message = "Teachers retrieved successfully",
            Data = teacherDto
        };
    }
}