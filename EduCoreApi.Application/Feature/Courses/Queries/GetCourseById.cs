using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Courses.Models;
using EduCoreApi.Application.Feature.Courses.Repositories;
using EduCoreApi.Application.Feature.Courses.Specifications;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Courses.Queries;

public sealed record GetCourseById(Guid CourseId) : IRequest<ApiResponse<GetCourseDto>>;

public sealed class GetCourseByIdValidator : AbstractValidator<GetCourseById>
{
    public GetCourseByIdValidator()
    {
        RuleFor(x => x.CourseId)
            .GreaterThan(Guid.Empty);
    }
}

internal sealed class GetCourseByIdHendler(ICourseRepository courseRepository, IMapper mapper)
    : IRequestHandler<GetCourseById, ApiResponse<GetCourseDto>>
{
    public async Task<ApiResponse<GetCourseDto>> Handle(GetCourseById request, CancellationToken cancellationToken)
    {
        var spec = new CourseByIdSpec(request.CourseId, asNoTracking: true);

        var course = await courseRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (course == null)
        {
            return new ApiResponse<GetCourseDto>
            {
                Code = 404,
                Message = "Course no found",
                Data = null
            };
        }

        var getGroupDto = mapper.Map<GetCourseDto>(course);

        return new ApiResponse<GetCourseDto>
        {
            Code = 200,
            Message = "Course retrieved successfully",
            Data = getGroupDto
        };
    }
}