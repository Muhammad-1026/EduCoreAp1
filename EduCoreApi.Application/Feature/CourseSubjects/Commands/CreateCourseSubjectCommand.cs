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

internal sealed class CreateCourseSubjectCommandHandler(ICourseSubjectRepository courseSubjectRepository, IMapper mapper) : IRequestHandler<CreateCourseSubjectCommand, ApiResponse<CreateCourseSubjectDto>>
{
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

        await courseSubjectRepository.AddAsync(courseSubject, cancellationToken);
        await courseSubjectRepository.SaveChangesAsync(cancellationToken);

        var createGroupDto = mapper.Map<CreateCourseSubjectDto>(courseSubject);

        return new ApiResponse<CreateCourseSubjectDto>
        {
            Code = 200,
            Message = "CourseSubject created successfully",
            Data = createGroupDto
        };
    }
}