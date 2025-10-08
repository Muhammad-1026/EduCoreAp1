using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Students.Repositories;
using EduCoreApi.Application.Feature.Students.Specifications;
using EduCoreApi.Shared.Exeptions;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Students.Commands;

public sealed record DeleteStudentCommand(Guid StudentId) : IRequest<ApiResponse>;

public sealed class DeleteStudentCommandValidator : AbstractValidator<DeleteStudentCommand>
{
    public DeleteStudentCommandValidator()
    {
        RuleFor(x => x.StudentId).GreaterThan(Guid.Empty);
    }
}

internal sealed class DeleteStudentCommandHandler(IStudentRepository studentRepository) : IRequestHandler<DeleteStudentCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await studentRepository.FirstOrDefaultAsync(new StudentByIdSpec(request.StudentId), cancellationToken);

        if (student is null)
            return new ApiResponse
            {
                Code = 404,
                Message = "Student not found"
            };

        await studentRepository.DeleteAsync(student, cancellationToken);
        await studentRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse
        {
            Code = 200,
            Message = "Student deleted successfully"
        };
    }
}