using EduCoreApi.Application.Feature.Teachers.Specifications;
using EduCoreApi.Application.Feature.Teachers.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Shared.Models;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Teachers.Commands;

public sealed record UpdateTeacherCommand(Guid TeacherId,
    string FullName,
    DateTime BirthDate,
    string PhoneNumber,
    string Address,
    Gender Gender,
    bool IsActive,
    Guid DepartmentId,
    string ImageURL)
    : IRequest<ApiResponse>;

public sealed class UpdateTeacherCommandValidator : AbstractValidator<UpdateTeacherCommand>
{
    public UpdateTeacherCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty();
        RuleFor(x => x.BirthDate)
            .NotEmpty();
        RuleFor(x => x.PhoneNumber)
            .NotEmpty();
        RuleFor(x => x.Address)
            .NotEmpty();
        RuleFor(x => x.Gender)
            .NotEmpty();
        RuleFor(x => x.IsActive)
            .NotEmpty();
        RuleFor(x => x.DepartmentId)
            .NotEmpty();
        RuleFor(x => x.ImageURL)
            .NotEmpty();
    }
}

internal sealed class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, ApiResponse>
{
    private readonly ITeacherRepository _teacherRepository;
    public UpdateTeacherCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<ApiResponse> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.FirstOrDefaultAsync(new TeacherByIdSpec(request.DepartmentId), cancellationToken);

        if (teacher is null)
        {
            return new ApiResponse
            {
                Code = 404,
                Message = "Teacher not found"
            };
        }

        teacher.SetFullName(request.FullName);
        teacher.SetBirthDate(request.BirthDate);
        teacher.SetPhoneNumber(request.PhoneNumber);
        teacher.SetAddress(request.Address);
        teacher.SetGender(request.Gender);
        teacher.SetIsActive(request.IsActive);
        teacher.SetDepartmentId(request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.ImageURL))
            teacher.SetImageURL(request.ImageURL);

        await _teacherRepository.UpdateAsync(teacher, cancellationToken);
        await _teacherRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse
        {
            Code = 200,
            Message = "Teacher updated successfully"
        };
    }
}
