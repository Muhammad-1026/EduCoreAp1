using EduCoreApi.Application.Feature.Teachers.Repositories;
using EduCoreApi.Application.Feature.Teachers.Models;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Domain.Models;
using EduCoreApi.Shared.Models;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Teachers.Commands;

public sealed record CreateTeacherCommmand(string FullName,
    DateTime BirthDate,
    string PhoneNumber,
    string Address,
    Gender Gender,
    bool IsActive,
    Guid DepartmentId,
    string ImageURL)
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
        //RuleFor(x => x.DepartmentId)
        //    .NotEmpty();
        //RuleFor(x => x.ImageURL)
        //    .NotEmpty();
    }
}

internal sealed class CrateTeacherCommmandHandler : IRequestHandler<CreateTeacherCommmand, ApiResponse<CreateTeacherDto>>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;

    public CrateTeacherCommmandHandler(ITeacherRepository teacherRepository, IMapper mapper)
    {
        _teacherRepository = teacherRepository;
        _mapper = mapper;
    }

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

        await _teacherRepository.AddAsync(teacher);
        await _teacherRepository.SaveChangesAsync(cancellationToken);

        var teacherDto = _mapper.Map<CreateTeacherDto>(teacher);

        return new ApiResponse<CreateTeacherDto>
        {
            Code = 201,
            Message = "",
            Data = teacherDto
        };
    }
}