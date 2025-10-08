using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Students.Repositories;
using EduCoreApi.Application.Feature.Students.Specifications;
using EduCoreApi.Shared.Models;
using FluentValidation;
using MediatR;

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
    Guid SpecialityId
    ) : IRequest<ApiResponse<Guid>>;

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

internal sealed class UpdateStudentCommandHandler(IStudentRepository studentRepository) : IRequestHandler<UpdateStudentCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var speciality = await studentRepository.FirstOrDefaultAsync(new StudentByIdSpec(request.StudentId), cancellationToken);

        if (speciality is null)
        {
            return new ApiResponse<Guid>
            {
                Code = 404,
                Message = "Speciality not found"
            };
        }
        speciality.SetEmail(request.Email);
        speciality.SetImageUrl(request.ImageUrl);
        speciality.SetFullName(request.FullName);
        speciality.SetBirthDate(request.BirthDate);
        speciality.SetPhoneNumber(request.PhoneNumber);
        speciality.SetAddress(request.Address);
        speciality.SetIsDormitoryResident(request.IsDormitoryResident);
        speciality.SetGender(request.Gender);
        speciality.SetIsActive(request.IsActive);
        speciality.SetGroupId(request.GroupId);
        speciality.SetSpecialityId(request.SpecialityId);

        await studentRepository.UpdateAsync(speciality, cancellationToken);
        await studentRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Guid>
        {
            Code = 200,
            Message = "Speciality updated successfully",
            Data = speciality.Id,
        };
    }
}