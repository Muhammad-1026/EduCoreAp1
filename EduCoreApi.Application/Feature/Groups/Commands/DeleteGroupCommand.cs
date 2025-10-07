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

internal sealed class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, ApiResponse>
{
    private readonly IGroupRepository _groupRepository;

    public DeleteGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<ApiResponse> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var speciality = await _groupRepository.FirstOrDefaultAsync(new GroupByIdSpec(request.GroupId), cancellationToken);

        if (speciality is null)
        {
            return new ApiResponse
            {
                Code = 404,
                Message = "Speciality not found"
            };
        }

        await _groupRepository.DeleteAsync(speciality, cancellationToken);
        await _groupRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse
        {
            Code = 200,
            Message = "Speciality deleted successfully",
        };
    }
}