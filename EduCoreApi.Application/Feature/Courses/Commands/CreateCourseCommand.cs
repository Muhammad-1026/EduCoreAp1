using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Courses.Models;
using EduCoreApi.Domain.Models;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Courses.Commands;

public sealed record CreateCuorseCommand(string Name, string? Description) : IRequest<ApiResponse<CreateCourseDto>>;

public sealed class CreateCuorseCommandValidator : AbstractValidator<CreateCuorseCommand>
{
    public CreateCuorseCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}

internal sealed class CreateCurseCommandHandler(ICourseRepository courseRepository, IMapper mapper)
    : IRequestHandler<CreateCuorseCommand, ApiResponse<CreateCourseDto>>
{
    public async Task<ApiResponse<CreateCourseDto>> Handle(CreateCuorseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course(
            request.Name,
            Guid.Empty
        );

        course.SetDescription(request.Description);

        await courseRepository.AddAsync(course, cancellationToken);
        await courseRepository.SaveChangesAsync(cancellationToken);

        var createGroupDto = mapper.Map<CreateCourseDto>(course);

        return new ApiResponse<CreateCourseDto>
        {
            Code = 200,
            Message = "Group created successfully",
            Data = createGroupDto
        };
    }
}
