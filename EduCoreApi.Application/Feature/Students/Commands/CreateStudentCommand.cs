using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Feature.Students.Models;
using EduCoreApi.Domain.Models;
using EduCoreApi.Shared.Models;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Students.Commands;

public sealed record CreateStudentCommand(string FullName,
    DateTime BirthDate,
    string? Email,
    string PhoneNumber,
    string Address,
    bool IsDormitoryResident,
    string? ImageUrl,
    Gender Gender,
    bool IsActive,
    Guid GroupId,
    Guid SpecialityId
    ) : IRequest<CreateStudentDto>;

public sealed class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));
        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));

        RuleFor(x => x.FullName)
            .NotEmpty();
        RuleFor(x => x.BirthDate)
            .NotEmpty();
        RuleFor(x => x.PhoneNumber)
            .NotEmpty();
        RuleFor(x => x.Address)
            .NotEmpty();
        RuleFor(x => x.IsDormitoryResident)
            .NotEmpty();
    }
}

internal sealed class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, CreateStudentDto>
{
    private readonly IStudentRepository studentRepository;
    private readonly IMediator _mediator;
    private readonly TimeProvider _timeProvider;

    public CreateStudentCommandHandler(IStudentRe pository studentRepository, IMediator mediator, TimeProvider timeProvider)
    {
        this.studentRepository = studentRepository;
        _mediator = mediator;
        _timeProvider = timeProvider;
    }

    public async Task<CreateStudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student(request.FullName)
        {
            CreatedDate = _timeProvider.GetLocalNow().DateTime,
        }

    }
}
