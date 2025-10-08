using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.CourseTeachers.Specifications;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.CourseTeachers.Commands;

public sealed record UpdateCourseTeacherCommand(Guid CourseId, Guid TeacherId, bool IsCurator) : IRequest<ApiResponse<Guid>>;

public sealed class UpdateCourseTeacherCommandValidator : AbstractValidator<UpdateCourseTeacherCommand>
{
    public UpdateCourseTeacherCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty();
        RuleFor(x => x.TeacherId)
            .NotEmpty();
        RuleFor(x => x.IsCurator)
            .NotEmpty();
    }
}

internal sealed class UpdateCourseTeacherCommandHandler(ICourseTeacherRepository courseTeacherRepository) : IRequestHandler<UpdateCourseTeacherCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(UpdateCourseTeacherCommand request, CancellationToken cancellationToken)
    {
        var courseSubject = await courseTeacherRepository.FirstOrDefaultAsync(new CourseTeacherByIdSpec(request.CourseId), cancellationToken);

        if (courseSubject is null)
        {
            return new ApiResponse<Guid>
            {
                Code = 404,
                Message = "CourseTeacher not found"
            };
        }

        courseSubject.SetCourseId(request.CourseId);
        courseSubject.SetTeacherId(request.TeacherId);
        courseSubject.SetIsCurator(request.IsCurator);

        await courseTeacherRepository.UpdateAsync(courseSubject, cancellationToken);
        await courseTeacherRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Guid>
        {
            Code = 200,
            Message = "CourseTeacher updated successfully",
            Data = courseSubject.Id,
        };
    }
}