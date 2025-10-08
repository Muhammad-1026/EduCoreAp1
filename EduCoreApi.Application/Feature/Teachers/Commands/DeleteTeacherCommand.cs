using EduCoreApi.Application.Feature.Teachers.Specifications;
using EduCoreApi.Application.Common.Results;
using FluentValidation;
using MediatR;
using EduCoreApi.Application.Common.Repositories;

namespace EduCoreApi.Application.Feature.Teachers.Commands;

public sealed record DeleteTeacherCommand(Guid TeacherId) : IRequest<ApiResponse>;

public sealed class DeleteTeacherCommandValidator : AbstractValidator<DeleteTeacherCommand>
{
    public DeleteTeacherCommandValidator()
    {
        RuleFor(x => x.TeacherId)
            .GreaterThan(Guid.Empty);
    }
}

internal sealed class DeleteTeacherCommandHandler(ITeacherRepository teacherRepository) : IRequestHandler<DeleteTeacherCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await teacherRepository.FirstOrDefaultAsync(new TeacherByIdSpec(request.TeacherId), cancellationToken);

        if (teacher == null)
            return new ApiResponse
            {
                Code = 404,
                Message = "Teacher not found",
            };

        await teacherRepository.DeleteAsync(teacher);
        await teacherRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse
        {
            Code = 200,
            Message = "Teacher deleted successfully",
        };
    }
}