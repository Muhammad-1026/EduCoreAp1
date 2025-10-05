using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.CourseSubjects.Models;
using EduCoreApi.Application.Feature.CourseSubjects.Specifications;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.CourseSubjects.Queries;

public sealed record GetCourseSubjectById(Guid CourseSubjectId) : IRequest<ApiResponse<GetCourseSubjectDto>>;

public sealed class GetCourseSubjectByIdValidator : AbstractValidator<GetCourseSubjectById>
{
    public GetCourseSubjectByIdValidator()
    {
        RuleFor(x => x.CourseSubjectId)
            .GreaterThan(Guid.Empty);
    }
}

internal sealed class GetCourseSubjectByIdHendler : IRequestHandler<GetCourseSubjectById, ApiResponse<GetCourseSubjectDto>>
{
    private readonly ICourseSubjectRepository _courseSubjectRepository;
    private readonly IMapper _mapper;

    public GetCourseSubjectByIdHendler(ICourseSubjectRepository courseSubjectRepository, IMapper mapper)
    {
        _courseSubjectRepository = courseSubjectRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<GetCourseSubjectDto>> Handle(GetCourseSubjectById request, CancellationToken cancellationToken)
    {
        var spec = new CourseSubjectByIdSpec(request.CourseSubjectId, asNoTracking: true);

        var courseSubject = await _courseSubjectRepository.FirstOrDefaultAsync(spec, cancellationToken);


        if (courseSubject is null)
        {
            return new ApiResponse<GetCourseSubjectDto>
            {
                Code = 404,
                Message = "CourseSubject no found",
                Data = null
            };
        }

        var getGroupDto = _mapper.Map<GetCourseSubjectDto>(courseSubject);

        return new ApiResponse<GetCourseSubjectDto>
        {
            Code = 200,
            Message = "CourseSubject retrieved successfully",
            Data = getGroupDto
        };
    }
}