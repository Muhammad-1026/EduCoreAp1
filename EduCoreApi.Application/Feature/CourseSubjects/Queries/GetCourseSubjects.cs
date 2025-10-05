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

internal sealed class GetCourseSubjectsHendler : IRequestHandler<GetCourseSubjects, ApiResponse<List<GetCourseSubjectDto>>>
{
    private readonly ICourseSubjectRepository _courseSubjectRepository;
    private readonly IMapper _mapper;

    public GetCourseSubjectsHendler(ICourseSubjectRepository courseSubjectRepository, IMapper mapper)
    {
        _courseSubjectRepository = courseSubjectRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<GetCourseSubjectDto>>> Handle(GetCourseSubjects request, CancellationToken cancellationToken)
    {
        var spec = new DbSpecifications<CourseSubject>();

        spec
            .Query
            .AsNoTracking()
            .Include(a => a.Course)
            .Include(a => a.Subject);

        var specialities = await _courseSubjectRepository.ListAsync(spec, cancellationToken);

        if (specialities == null)
        {
            return new ApiResponse<List<GetCourseSubjectDto>>
            {
                Code = 404,
                Message = "CourseSubject no found",
                Data = null
            };
        }

        var specialityDto = _mapper.Map<List<GetCourseSubjectDto>>(specialities);

        return new ApiResponse<List<GetCourseSubjectDto>>
        {
            Code = 200,
            Message = "CourseSubject retrieved successfully",
            Data = specialityDto
        };
    }
}