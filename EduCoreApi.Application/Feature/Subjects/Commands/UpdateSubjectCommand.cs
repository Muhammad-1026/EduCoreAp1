using EduCoreApi.Application.Feature.Subjects.Specifications;
using EduCoreApi.Application.Feature.Subjects.Repositories;
using EduCoreApi.Application.Feature.Subjects.Models;
using EduCoreApi.Application.Common.Results;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Subjects.Commands;

public sealed record UpdateSubjectCommand(Guid SubjectId, string Name, string? Description) : IRequest<ApiResponse<UpdateSubjectDto>>;

public sealed class UpdateSubjectCommandValidator : AbstractValidator<UpdateSubjectCommand>
{
    public UpdateSubjectCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.SubjectId)
        .GreaterThan(Guid.Empty);
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}

public sealed class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, ApiResponse<UpdateSubjectDto>>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;

    public UpdateSubjectCommandHandler(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<UpdateSubjectDto>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.FirstOrDefaultAsync(new SubjectByIdSpec(request.SubjectId), cancellationToken);

        if (subject is null)
            return new ApiResponse<UpdateSubjectDto>
            {
                Code = 404,
                Message = "Subject not found"
            };

        subject.SetName(request.Name);

        if (!string.IsNullOrWhiteSpace(request.Description))
            subject.SetDescription(request.Description);

        await _subjectRepository.UpdateAsync(subject, cancellationToken);
        await _subjectRepository.SaveChangesAsync(cancellationToken);

        var updateSubjectDto = _mapper.Map<UpdateSubjectDto>(subject);

        return new ApiResponse<UpdateSubjectDto>
        {
            Code = 200,
            Message = "",
            Data = updateSubjectDto
        };
    }
}
