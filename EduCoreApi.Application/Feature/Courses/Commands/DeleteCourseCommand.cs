using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Courses.Repositories;
using EduCoreApi.Application.Feature.Courses.Specifications;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Courses.Commands;

public sealed record DeleteCourseCommand(Guid CourseId) : IRequest<ApiResponse>;

public sealed class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
{
    public DeleteCourseCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty();
    }
}

internal sealed class DeleteGroupCommandHandler(ICourseRepository courseRepository)
    : IRequestHandler<DeleteCourseCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var speciality = await courseRepository.FirstOrDefaultAsync(new CourseByIdSpec(request.CourseId), cancellationToken);

        if (speciality is null)
        {
            return new ApiResponse
            {
                Code = 404,
                Message = "Course not found"
            };
        }

        await courseRepository.DeleteAsync(speciality, cancellationToken);
        await courseRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse
        {
            Code = 200,
            Message = "Course deleted successfully",
        };
    }
}