using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.CourseSubjects.Specifications;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.CourseSubjects.Commands;

public sealed record UpdateCourseSubjectCommand(Guid CourseId, Guid SubjectId, int Credits, int Hours) : IRequest<ApiResponse<Guid>>;

public sealed class UpdateCourseSubjectCommandValidator : AbstractValidator<UpdateCourseSubjectCommand>
{
    public UpdateCourseSubjectCommandValidator()
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

internal sealed class UpdateCourseSubjectCommandHandler(ICourseSubjectRepository courseSubjectRepository) : IRequestHandler<UpdateCourseSubjectCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(UpdateCourseSubjectCommand request, CancellationToken cancellationToken)
    {
        var courseSubject = await courseSubjectRepository.FirstOrDefaultAsync(new CourseSubjectByIdSpec(request.CourseId), cancellationToken);

        if (courseSubject is null)
        {
            return new ApiResponse<Guid>
            {
                Code = 404,
                Message = "CourseSubject not found"
            };
        }

        courseSubject.SetCourseIds(request.CourseId);
        courseSubject.SetSubjectIds(request.SubjectId);
        courseSubject.SetCredits(request.Credits);
        courseSubject.SetHours(request.Hours);

        await courseSubjectRepository.UpdateAsync(courseSubject, cancellationToken);
        await courseSubjectRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Guid>
        {
            Code = 200,
            Message = "CourseSubject updated successfully",
            Data = courseSubject.Id,
        };
    }
}