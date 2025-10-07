using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Subjects.Models;
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
            .MaximumLength(500);
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
        var group = new Subject(
            request.Name,
                // createdBy TODO: replace with actual user id
                Guid.Empty
        );

        group.SetDescription(request.Description);

        await _subjectRepository.AddAsync(group, cancellationToken);
        await _subjectRepository.SaveChangesAsync(cancellationToken);

        var createGroupDto = _mapper.Map<CreateSubjectDto>(group);

        return new ApiResponse<CreateSubjectDto>
        {
            Code = 200,
            Message = "Subject created successfully",
            Data = createGroupDto
        };
    }
}