using Ardalis.Specification;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Application.Feature.Courses.Models;
using EduCoreApi.Application.Feature.Courses.Repositories;
using EduCoreApi.Domain.Models;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Courses.Queries;

public sealed record GetCourses : IRequest<ApiResponse<List<GetCourseDto>>>;

internal sealed class GetCoursesHendler(ICourseRepository courseRepository, IMapper mapper)
    : IRequestHandler<GetCourses, ApiResponse<List<GetCourseDto>>>
{
    public async Task<ApiResponse<List<GetCourseDto>>> Handle(GetCourses request, CancellationToken cancellationToken)
    {
        var spec = new DbSpecifications<Course>();

        spec
            .Query
            .AsNoTracking();

        var courses = await courseRepository.ListAsync(spec, cancellationToken);

        var getCoursesyDto = mapper.Map<List<GetCourseDto>>(courses);

        return new ApiResponse<List<GetCourseDto>>
        {
            Code = 200,
            Message = "Courses retrieved successfully",
            Data = getCoursesyDto
        };
    }
}