using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Groups.Models;
using EduCoreApi.Domain.Models;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Groups.Commands;

public sealed record CreateGroupCommand(string Name, int CourseNumber, string? Description, Guid SpecialityId) : IRequest<ApiResponse<CreateGroupDto>>;

public sealed class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        RuleFor(x => x.CourseNumber)
            .GreaterThan(0);
        RuleFor(x => x.SpecialityId)
            .NotEmpty();
        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}

internal sealed class CreateGroupCommandHandler(IGroupRepository groupRepository, IMapper mapper) : IRequestHandler<CreateGroupCommand, ApiResponse<CreateGroupDto>>
{
    public async Task<ApiResponse<CreateGroupDto>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = new Group(
            request.Name,
            request.CourseNumber,
             // createdBy TODO: replace with actual user id
             Guid.Empty
        );
        group.SetDescription(request.Description);
        group.SetSpeciality(request.SpecialityId);

        await groupRepository.AddAsync(group, cancellationToken);
        await groupRepository.SaveChangesAsync(cancellationToken);

        var createGroupDto = mapper.Map<CreateGroupDto>(group);

        return new ApiResponse<CreateGroupDto>
        {
            Code = 200,
            Message = "Group created successfully",
            Data = createGroupDto
        };
    }
}