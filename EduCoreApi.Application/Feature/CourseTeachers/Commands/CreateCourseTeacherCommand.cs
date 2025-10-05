using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.CourseTeachers.Models;
using EduCoreApi.Domain.Models;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.CourseTeachers.Commands;

public sealed record CreateCourseTeacherCommand(Guid CourseId, Guid TeacherId, bool IsCurator) : IRequest<ApiResponse<CreateCourseTeacherDto>>;

public sealed class CreateCourseTeacherCommandValidator : AbstractValidator<CreateCourseTeacherCommand>
{
    public CreateCourseTeacherCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty();
        RuleFor(x => x.TeacherId)
            .NotEmpty();
        RuleFor(x => x.IsCurator)
            .NotEmpty();
    }
}

internal sealed class CreateCourseTeacherCommandHandler : IRequestHandler<CreateCourseTeacherCommand, ApiResponse<CreateCourseTeacherDto>>
{
    private readonly ICourseTeacherRepository _courseTeacherRepository;
    private readonly IMapper _mapper;

    public CreateCourseTeacherCommandHandler(ICourseTeacherRepository courseTeacherRepository, IMapper mapper)
    {
        _courseTeacherRepository = courseTeacherRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CreateCourseTeacherDto>> Handle(CreateCourseTeacherCommand request, CancellationToken cancellationToken)
    {
        var courseSubject = new CourseTeacher(
            request.CourseId,
            request.TeacherId,
            request.IsCurator,
             // createdBy TODO: replace with actual user id
             Guid.Empty
        );

        await _courseTeacherRepository.AddAsync(courseSubject, cancellationToken);
        await _courseTeacherRepository.SaveChangesAsync(cancellationToken);

        var createGroupDto = _mapper.Map<CreateCourseTeacherDto>(courseSubject);

        return new ApiResponse<CreateCourseTeacherDto>
        {
            Code = 200,
            Message = "CourseTeacher created successfully",
            Data = createGroupDto
        };
    }
}