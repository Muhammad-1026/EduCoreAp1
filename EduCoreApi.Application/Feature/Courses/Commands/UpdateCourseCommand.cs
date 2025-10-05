using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Courses.Repositories;
using EduCoreApi.Application.Feature.Courses.Specifications;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Courses.Commands;

public sealed record UpdateCourseCommand(Guid CourseId, string Name, string? Description) : IRequest<ApiResponse<Guid>>;

public sealed class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty();
        RuleFor(x => x.Name)
             .NotEmpty();
        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}

internal sealed class UpdateCurseCommandHandler(ICourseRepository courseRepository)
    : IRequestHandler<UpdateCourseCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var speciality = await courseRepository.FirstOrDefaultAsync(new CourseByIdSpec(request.CourseId), cancellationToken);

        if (speciality is null)
        {
            return new ApiResponse<Guid>
            {
                Code = 404,
                Message = "Speciality not found"
            };
        }

        speciality.SetName(request.Name);

        if (!string.IsNullOrWhiteSpace(request.Description))
            speciality.SetDescription(request.Description);

        await courseRepository.UpdateAsync(speciality, cancellationToken);
        await courseRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Guid>
        {
            Code = 200,
            Message = "Course updated successfully",
            Data = speciality.Id,
        };
    }
}