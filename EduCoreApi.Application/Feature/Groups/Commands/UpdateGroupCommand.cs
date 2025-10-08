using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Groups.Specifications;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Groups.Commands;

public sealed record UpdateGroupCommand(Guid GroupId, string Name, int CourseNumber, string? Description, Guid SpecialityId) : IRequest<ApiResponse<Guid>>;

public sealed class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
{
    public UpdateGroupCommandValidator()
    {
        RuleFor(x => x.GroupId)
            .NotEmpty();
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

internal sealed class UpdateGroupCommandHandler(IGroupRepository groupRepository) : IRequestHandler<UpdateGroupCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var speciality = await groupRepository.FirstOrDefaultAsync(new GroupByIdSpec(request.GroupId), cancellationToken);

        if (speciality is null)
        {
            return new ApiResponse<Guid>
            {
                Code = 404,
                Message = "Speciality not found"
            };
        }

        speciality.SetName(request.Name);
        speciality.SetCourseNumber(request.CourseNumber);
        speciality.SetSpeciality(request.SpecialityId);

        if (!string.IsNullOrWhiteSpace(request.Description))
            speciality.SetDescription(request.Description);

        await groupRepository.UpdateAsync(speciality, cancellationToken);
        await groupRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Guid>
        {
            Code = 200,
            Message = "Speciality updated successfully",
            Data = speciality.Id,
        };
    }
}