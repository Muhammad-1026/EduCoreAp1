using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Feature.Students.Specifications;
using EduCoreApi.Shared.Exeptions;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Students.Commands;

public sealed record DeleteStudentCommand(Guid StudentId) : IRequest;

public sealed class DeleteStudentCommandValidator : AbstractValidator<DeleteStudentCommand>
{
    public DeleteStudentCommandValidator()
    {
        RuleFor(x => x.StudentId).GreaterThan(Guid.Empty);
    }
}

internal sealed class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
{
    private readonly IStudentRepository _studentRepository;
    public DeleteStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.FirstOrDefaultAsync(new StudentByIdSpec(request.StudentId), cancellationToken);

        if (student is null)
            throw new ResourceNotFoundException(StudentError.NotFound);

        await _studentRepository.DeleteAsync(student, cancellationToken);
        await _studentRepository.SaveChangesAsync(cancellationToken);
    }
}