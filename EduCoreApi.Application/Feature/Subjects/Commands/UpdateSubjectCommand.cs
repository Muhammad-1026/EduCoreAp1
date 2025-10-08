using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Subjects.Specifications;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Subjects.Commands;

public sealed record UpdateSubjectCommand(Guid SubjectId, string Name, string? Description) : IRequest<ApiResponse<Guid>>;

public sealed class UpdateSubjectCommandValidator : AbstractValidator<UpdateSubjectCommand>
{
    public UpdateSubjectCommandValidator()
    {
        RuleFor(x => x.SubjectId)
            .NotEmpty();
        RuleFor(x => x.Name)
              .NotEmpty();
        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}

internal sealed class UpdateSubjectCommandHandler(ISubjectRepository subjectRepository) : IRequestHandler<UpdateSubjectCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        var speciality = await subjectRepository.FirstOrDefaultAsync(new SubjectByIdSpec(request.SubjectId), cancellationToken);

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

        await subjectRepository.UpdateAsync(speciality, cancellationToken);
        await subjectRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Guid>
        {
            Code = 200,
            Message = "Speciality updated successfully",
            Data = speciality.Id,
        };
    }
}