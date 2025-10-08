using EduCoreApi.Application.Feature.Teachers.Models;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Domain.Models;
using EduCoreApi.Shared.Models;
using FluentValidation;
using MapsterMapper;
using MediatR;
using EduCoreApi.Application.Common.Repositories;

namespace EduCoreApi.Application.Feature.Teachers.Commands;

public sealed record CreateTeacherCommmand(string FullName,
    DateTime BirthDate,
    string PhoneNumber,
    string Address,
    Gender Gender,
    bool IsActive,
    Guid DepartmentId,
    string ImageURL,
    string Email)
    : IRequest<ApiResponse<CreateTeacherDto>>;

public sealed class CreateTeacherCommmandValidator : AbstractValidator<CreateTeacherCommmand>
{
    public CreateTeacherCommmandValidator()
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
        RuleFor(x => x.Email)
            .EmailAddress();
    }
}

internal sealed class CrateTeacherCommmandHandler(ITeacherRepository teacherRepository, IMapper mapper) : IRequestHandler<CreateTeacherCommmand, ApiResponse<CreateTeacherDto>>
{
    public async Task<ApiResponse<CreateTeacherDto>> Handle(CreateTeacherCommmand request, CancellationToken cancellationToken)
    {
        var teacher = new Teacher(
            request.FullName,
            request.BirthDate,
            request.PhoneNumber,
            request.Address,
            request.Gender,
            request.IsActive,
            request.DepartmentId,
            Guid.Empty
        );

        if (!string.IsNullOrWhiteSpace(request.ImageURL))
            teacher.SetImageURL(request.ImageURL);

        if (!string.IsNullOrWhiteSpace(request.Email))
            teacher.SetEmail(request.Email);

        await teacherRepository.AddAsync(teacher);
        await teacherRepository.SaveChangesAsync(cancellationToken);

        var teacherDto = mapper.Map<CreateTeacherDto>(teacher);

        return new ApiResponse<CreateTeacherDto>
        {
            Code = 201,
            Message = " ",
            Data = teacherDto
        };
    }
}