using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.CourseTeachers.Specifications;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.CourseTeachers.Commands;

public sealed record DeleteCourseTeacherCommand(Guid CourseTeacherId) : IRequest<ApiResponse>;

public sealed class DeleteCourseTeacherCommandValidator : AbstractValidator<DeleteCourseTeacherCommand>
{
    public DeleteCourseTeacherCommandValidator()
    {
        RuleFor(x => x.CourseTeacherId)
            .NotEmpty();
    }
}

internal sealed class DeleteCourseTeacherCommandHandler(ICourseTeacherRepository courseTeacherRepository) : IRequestHandler<DeleteCourseTeacherCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteCourseTeacherCommand request, CancellationToken cancellationToken)
    {
        var speciality = await courseTeacherRepository.FirstOrDefaultAsync(new CourseTeacherByIdSpec(request.CourseTeacherId), cancellationToken);

        if (speciality is null)
        {
            return new ApiResponse
            {
                Code = 404,
                Message = "CourseTeacher not found"
            };
        }

        await courseTeacherRepository.DeleteAsync(speciality, cancellationToken);
        await courseTeacherRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse
        {
            Code = 200,
            Message = "CourseTeacher deleted successfully",
        };
    }
}