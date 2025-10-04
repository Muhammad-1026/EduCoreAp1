using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Subjects.Repositories;
using EduCoreApi.Application.Feature.Subjects.Specifications;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Subjects.Commands;

public sealed record DeleteSubjectCommand(Guid SubjectId) : IRequest<ApiResponse>;

public sealed class DeleteSubjectCommandValidator : AbstractValidator<DeleteSubjectCommand>
{
    public DeleteSubjectCommandValidator()
    {
        RuleFor(x => x.SubjectId).NotEmpty();
    }
}

internal sealed class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, ApiResponse>
{
    private readonly ISubjectRepository _subjectRepository;

    public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<ApiResponse> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        var speciality = await _subjectRepository.FirstOrDefaultAsync(new SubjectByIdSpec(request.SubjectId), cancellationToken);

        if (speciality is null)
        {
            return new ApiResponse
            {
                Code = 404,
                Message = "Speciality not found"
            };
        }

        await _subjectRepository.DeleteAsync(speciality, cancellationToken);
        await _subjectRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse
        {
            Code = 200,
            Message = "Speciality deleted successfully",
        };
    }
}