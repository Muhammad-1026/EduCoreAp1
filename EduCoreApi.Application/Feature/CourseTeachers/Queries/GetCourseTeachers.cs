using Ardalis.Specification;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Application.Feature.CourseTeachers.Models;
using EduCoreApi.Domain.Models;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.CourseTeachers.Queries;

public sealed record GetCourseTeachers : IRequest<ApiResponse<List<GetCourseTeacherDto>>>;

internal sealed class GetCourseTeachersHendler : IRequestHandler<GetCourseTeachers, ApiResponse<List<GetCourseTeacherDto>>>
{
    private readonly ICourseTeacherRepository _courseTeacherRepository;
    private readonly IMapper _mapper;

    public GetCourseTeachersHendler(ICourseTeacherRepository courseTeacherRepository, IMapper mapper)
    {
        _courseTeacherRepository = courseTeacherRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<GetCourseTeacherDto>>> Handle(GetCourseTeachers request, CancellationToken cancellationToken)
    {
        var spec = new DbSpecifications<CourseTeacher>();

        spec
            .Query
            .AsNoTracking()
            .Include(a => a.Course)
            .Include(a => a.Teacher);

        var specialities = await _courseTeacherRepository.ListAsync(spec, cancellationToken);

        if (specialities == null)
        {
            return new ApiResponse<List<GetCourseTeacherDto>>
            {
                Code = 404,
                Message = "CourseTeacher not found",
                Data = null
            };
        }

        var specialityDto = _mapper.Map<List<GetCourseTeacherDto>>(specialities);

        return new ApiResponse<List<GetCourseTeacherDto>>
        {
            Code = 200,
            Message = "CourseTeacher retrieved successfully",
            Data = specialityDto
        };
    }
}