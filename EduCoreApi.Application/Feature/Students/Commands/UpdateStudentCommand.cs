using EduCoreApi.Shared.Models;
using FluentValidation;

namespace EduCoreApi.Application.Feature.Students.Commands;

public sealed record UpdateStudentCommand(Guid StudentId, 
    string FullName,
    string LastNameDateTime,
    DateTime DateOfBirth,
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

        RuleFor(x => x.FullName).NotEmpty();
    }
}
