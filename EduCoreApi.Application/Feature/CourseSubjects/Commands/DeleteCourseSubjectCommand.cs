using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.CourseSubjects.Specifications;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.CourseSubjects.Commands;

public sealed record DeleteCourseSubjectCommand(Guid CourseSubjectId) : IRequest<ApiResponse>;

public sealed class DeleteGroupCommandValidator : AbstractValidator<DeleteCourseSubjectCommand>
{
    public DeleteGroupCommandValidator()
    {
        RuleFor(x => x.CourseSubjectId)
            .NotEmpty();
    }
}

internal sealed class DeleteCourseSubjectCommandHandler(ICourseSubjectRepository courseSubjectRepository) : IRequestHandler<DeleteCourseSubjectCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteCourseSubjectCommand request, CancellationToken cancellationToken)
    {
        var speciality = await courseSubjectRepository.FirstOrDefaultAsync(new CourseSubjectByIdSpec(request.CourseSubjectId), cancellationToken);

        if (speciality is null)
        {
            return new ApiResponse
            {
                Code = 404,
                Message = "CourseSubject not found"
            };
        }

        await courseSubjectRepository.DeleteAsync(speciality, cancellationToken);
        await courseSubjectRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse
        {
            Code = 200,
            Message = "CourseSubject deleted successfully",
        };
    }
}