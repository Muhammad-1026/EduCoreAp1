using EduCoreApi.Application.Feature.Subjects.Repositories;
using EduCoreApi.Application.Feature.Subjects.Models;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Domain.Models;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Subjects.Commands;

public sealed record CreateSubjectCommand(string Name, string? Description) : IRequest<ApiResponse<CreateSubjectDto>>;

public sealed class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
{
    public CreateSubjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty()
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}

internal sealed class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, ApiResponse<CreateSubjectDto>>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;

    public CreateSubjectCommandHandler(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CreateSubjectDto>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = new Subject(
            name: request.Name,
            createdBy: new Guid(),
            description: request.Description
        );

        if (subject is null)
            return new ApiResponse<CreateSubjectDto>
            {
                Code = 404,
                Message = "Subject not found"
            };

        await _subjectRepository.AddAsync(subject, cancellationToken);
        await _subjectRepository.SaveChangesAsync(cancellationToken);

        var subjectDto = _mapper.Map<CreateSubjectDto>(subject);

        return new ApiResponse<CreateSubjectDto>
        {
            Code = 200,
            Message = "",
            Data = subjectDto
        };
    }
}