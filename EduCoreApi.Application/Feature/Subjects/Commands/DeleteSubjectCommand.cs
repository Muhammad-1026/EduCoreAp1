using EduCoreApi.Application.Common.Results;
using FluentValidation;
using MediatR;
using EduCoreApi.Application.Common.Repositories;

namespace EduCoreApi.Application.Feature.Subjects.Commands;

public sealed record DeleteSubjectCommand(Guid SubjectId) : IRequest<ApiResponse<Guid>>;


public sealed class DeleteSubjectCommandValidator : AbstractValidator<DeleteSubjectCommand>
{
    public DeleteSubjectCommandValidator()
    {
        RuleFor(x => x.SubjectId)
            .GreaterThan(Guid.Empty);
    }
}

public sealed class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, ApiResponse<Guid>>
{
    private readonly ISubjectRepository _subjectRepository;

    public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<ApiResponse<Guid>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}