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

internal sealed class DeleteCourseTeacherCommandHandler : IRequestHandler<DeleteCourseTeacherCommand, ApiResponse>
{
    private readonly ICourseTeacherRepository _courseTeacherRepository;

    public DeleteCourseTeacherCommandHandler(ICourseTeacherRepository courseTeacherRepository)
    {
        _courseTeacherRepository = courseTeacherRepository;
    }

    public async Task<ApiResponse> Handle(DeleteCourseTeacherCommand request, CancellationToken cancellationToken)
    {
        var speciality = await _courseTeacherRepository.FirstOrDefaultAsync(new CourseTeacherByIdSpec(request.CourseTeacherId), cancellationToken);

        if (speciality is null)
        {
            return new ApiResponse
            {
                Code = 404,
                Message = "CourseTeacher not found"
            };
        }

        await _courseTeacherRepository.DeleteAsync(speciality, cancellationToken);
        await _courseTeacherRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse
        {
            Code = 200,
            Message = "CourseTeacher deleted successfully",
        };
    }
}