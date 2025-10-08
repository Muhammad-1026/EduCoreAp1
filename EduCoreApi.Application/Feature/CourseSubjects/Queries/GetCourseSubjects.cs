using Ardalis.Specification;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Application.Feature.CourseSubjects.Models;
using EduCoreApi.Domain.Models;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.CourseSubjects.Queries;

public sealed record GetCourseSubjects : IRequest<ApiResponse<List<GetCourseSubjectDto>>>;

internal sealed class GetCourseSubjectsHendler(ICourseSubjectRepository courseSubjectRepository, IMapper mapper) : IRequestHandler<GetCourseSubjects, ApiResponse<List<GetCourseSubjectDto>>>
{
    public async Task<ApiResponse<List<GetCourseSubjectDto>>> Handle(GetCourseSubjects request, CancellationToken cancellationToken)
    {
        var spec = new DbSpecifications<CourseSubject>();

        spec
            .Query
            .AsNoTracking()
            .Include(a => a.Course)
            .Include(a => a.Subject);

        var specialities = await courseSubjectRepository.ListAsync(spec, cancellationToken);courseSubjectRepository
        if (specialities == null)
        {
            return new ApiResponse<List<GetCourseSubjectDto>>
            {
                Code = 404,
                Message = "CourseSubject no found",
                Data = null
            };
        }

        var specialityDto = mapper.Map<List<GetCourseSubjectDto>>(specialities);

        return new ApiResponse<List<GetCourseSubjectDto>>
        {
            Code = 200,
            Message = "CourseSubject retrieved successfully",
            Data = specialityDto
        };
    }
}