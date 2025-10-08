using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.CourseTeachers.Models;
using EduCoreApi.Application.Feature.CourseTeachers.Specifications;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.CourseTeachers.Queries;

public sealed record GetCourseTeacherById(Guid CourseTeacherId) : IRequest<ApiResponse<GetCourseTeacherDto>>;

public sealed class GetCourseTeacherByIdValidator : AbstractValidator<GetCourseTeacherById>
{
    public GetCourseTeacherByIdValidator()
    {
        RuleFor(x => x.CourseTeacherId)
            .GreaterThan(Guid.Empty);
    }
}

internal sealed class GetCourseTeacherByIdHendler(ICourseTeacherRepository courseTeacherRepository, IMapper mapper) : IRequestHandler<GetCourseTeacherById, ApiResponse<GetCourseTeacherDto>>
{
    public async Task<ApiResponse<GetCourseTeacherDto>> Handle(GetCourseTeacherById request, CancellationToken cancellationToken)
    {
        var spec = new CourseTeacherByIdSpec(request.CourseTeacherId, asNoTracking: true);

        var courseSubject = await courseTeacherRepository.FirstOrDefaultAsync(spec, cancellationToken);


        if (courseSubject is null)
        {
            return new ApiResponse<GetCourseTeacherDto>
            {
                Code = 404,
                Message = "CourseTeacher no found",
                Data = null
            };
        }

        var getGroupDto = mapper.Map<GetCourseTeacherDto>(courseSubject);

        return new ApiResponse<GetCourseTeacherDto>
        {
            Code = 200,
            Message = "CourseTeacher retrieved successfully",
            Data = getGroupDto
        };
    }
}