using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Groups.Specifications;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Groups.Commands;

public sealed record DeleteGroupCommand(Guid GroupId) : IRequest<ApiResponse>;

public sealed class DeleteGroupCommandValidator : AbstractValidator<DeleteGroupCommand>
{
    public DeleteGroupCommandValidator()
    {
        RuleFor(x => x.GroupId).NotEmpty();
    }
}

internal sealed class DeleteGroupCommandHandler(IGroupRepository groupRepository) : IRequestHandler<DeleteGroupCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var speciality = await groupRepository.FirstOrDefaultAsync(new GroupByIdSpec(request.GroupId), cancellationToken);

        if (speciality is null)
        {
            return new ApiResponse
            {
                Code = 404,
                Message = "Speciality not found"
            };
        }

        await groupRepository.DeleteAsync(speciality, cancellationToken);
        await groupRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse
        {
            Code = 200,
            Message = "Speciality deleted successfully",
        };
    }
}