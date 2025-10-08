using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Students.Models;
using EduCoreApi.Application.Feature.Students.Repositories;
using EduCoreApi.Domain.Models;
using EduCoreApi.Shared.Models;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Students.Commands;

public sealed record CreateStudentCommand(string FullName,
    DateTime BirthDate,
    int Age,
    string? Email,
    string PhoneNumber,
    string Address,
    bool IsDormitoryResident,
    string? ImageUrl,
    Gender Gender,
    bool IsActive,
    Guid GroupId,
    Guid SpecialityId
    ) : IRequest<ApiResponse<CreateStudentDto>>;

public sealed class CreateStudentCommandValidator : AbstractValidator< CreateStudentCommand>
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
        RuleFor(x => x.Age)
            .GreaterThan(0);
        RuleFor(x => x.PhoneNumber)
            .NotEmpty();
        RuleFor(x => x.Address)
            .NotEmpty();
        RuleFor(x => x.IsDormitoryResident)
            .NotEmpty();
    }
}

internal sealed class CreateStudentCommandHandler(IStudentRepository studentRepository, IMapper mapper) : IRequestHandler<CreateStudentCommand, ApiResponse<CreateStudentDto>>
{
    public async Task<ApiResponse<CreateStudentDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student(
            request.FullName,
            request.BirthDate,
            request.Age,
            request.PhoneNumber,
            request.Address,
            request.IsDormitoryResident,
            request.Gender,
            request.GroupId,
            request.SpecialityId,
            request.IsActive,
            createdBy: Guid.Empty
        );

        if (!string.IsNullOrWhiteSpace(request.Email))
            student.SetEmail(request.Email);

        if (!string.IsNullOrWhiteSpace(request.ImageUrl))
            student.SetImageUrl(request.ImageUrl);

        await studentRepository.AddAsync(student, cancellationToken);
        await studentRepository.SaveChangesAsync(cancellationToken);

        var createStudentDto = mapper.Map<CreateStudentDto>(student);
 
        return new ApiResponse<CreateStudentDto>
        {
            Code = 200,
            Message = "Student created successfully",
            Data = createStudentDto
        };
    }
}