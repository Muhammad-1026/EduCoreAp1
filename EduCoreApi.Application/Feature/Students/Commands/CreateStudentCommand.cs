using EduCoreApi.Application.Feature.Students.Models;
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