using EduCoreApi.Application.Feature.Teachers.Specifications;
using EduCoreApi.Application.Feature.Teachers.Repositories;
using EduCoreApi.Application.Common.Results;
using FluentValidation;
using MediatR;

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

internal sealed class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, ApiResponse>
{
    private ITeacherRepository _teacherRepository;

    public DeleteTeacherCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<ApiResponse> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.FirstOrDefaultAsync(new TeacherByIdSpec(request.TeacherId), cancellationToken);

        if (teacher == null)
            return new ApiResponse
            {
                Code = 404,
                Message = "Teacher not found",
            };

        await _teacherRepository.DeleteAsync(teacher);
        await _teacherRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse
        {
            Code = 200,
            Message = "Teacher deleted successfully",
        };
    }
}