using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.CourseSubjects.Models;
using EduCoreApi.Domain.Models;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.CourseSubjects.Commands;

public sealed record CreateCourseSubjectCommand(Guid CourseId, Guid SubjectId, int Credits, int Hours) : IRequest<ApiResponse<CreateCourseSubjectDto>>;

public sealed class CreateCourseSubjectCommandValidator : AbstractValidator<CreateCourseSubjectCommand>
{
    public CreateCourseSubjectCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty();
        RuleFor(x => x.SubjectId)
            .NotEmpty();
        RuleFor(x => x.Credits)
            .NotEmpty();
        RuleFor(x => x.Hours)
            .NotEmpty();
    }
}

internal sealed class CreateCourseSubjectCommandHandler : IRequestHandler<CreateCourseSubjectCommand, ApiResponse<CreateCourseSubjectDto>>
{
    private readonly ICourseSubjectRepository _courseSubjectRepository;
    private readonly IMapper _mapper;

    public CreateCourseSubjectCommandHandler(ICourseSubjectRepository courseSubjectRepository, IMapper mapper)
    {
        _courseSubjectRepository = courseSubjectRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CreateCourseSubjectDto>> Handle(CreateCourseSubjectCommand request, CancellationToken cancellationToken)
    {
        var courseSubject = new CourseSubject(
            request.CourseId,
            request.SubjectId,
            request.Credits,
            request.Hours,
             // createdBy TODO: replace with actual user id
             Guid.Empty
        );

        await _courseSubjectRepository.AddAsync(courseSubject, cancellationToken);
        await _courseSubjectRepository.SaveChangesAsync(cancellationToken);

        var createGroupDto = _mapper.Map<CreateCourseSubjectDto>(courseSubject);

        return new ApiResponse<CreateCourseSubjectDto>
        {
            Code = 200,
            Message = "CourseSubject created successfully",
            Data = createGroupDto
        };
    }
}