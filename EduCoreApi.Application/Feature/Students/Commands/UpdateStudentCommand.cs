using EduCoreApi.Shared.Models;
using FluentValidation;

namespace EduCoreApi.Application.Feature.Students.Commands;

public sealed record UpdateStudentCommand(Guid StudentId,
    string FullName,
    DateTime BirthDate,
    string? Email,
    string PhoneNumber,
    string Address,
    bool IsDormitoryResident,
    string? ImageUrl,
    Gender Gender,
    bool IsActive,
    Guid GroupId,
    Guid SpecialityId);

public sealed class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.StudentId).GreaterThan(Guid.Empty);

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
        RuleFor(x => x.Gender)
            .NotEmpty();
        RuleFor(x => x.IsActive)
            .NotEmpty();
        RuleFor(x => x.GroupId)
            .GreaterThan(Guid.Empty);
        RuleFor(x => x.SpecialityId)
            .GreaterThan(Guid.Empty);
    }
}