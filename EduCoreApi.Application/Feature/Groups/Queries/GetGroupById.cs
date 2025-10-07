using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Groups.Models;
using EduCoreApi.Application.Feature.Groups.Specifications;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Groups.Queries;

public sealed record GetGroupById(Guid GroupId) : IRequest<ApiResponse<GetGroupDto>>;

public sealed class GetGroupByIdValidator : AbstractValidator<GetGroupById>
{
    public GetGroupByIdValidator()
    {
        RuleFor(x => x.GroupId)
            .GreaterThan(Guid.Empty);
    }
}

internal sealed class GetGroupByIdHendler : IRequestHandler<GetGroupById, ApiResponse<GetGroupDto>>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public GetGroupByIdHendler(IGroupRepository groupRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<GetGroupDto>> Handle(GetGroupById request, CancellationToken cancellationToken)
    {
        var spec = new GroupByIdSpec(request.GroupId, asNoTracking: true);

        var group = await _groupRepository.FirstOrDefaultAsync(spec, cancellationToken);


        if (group is null)
        {
            return new ApiResponse<GetGroupDto>
            {
                Code = 404,
                Message = "Group no found"
            };
        }

        var getGroupDto = _mapper.Map<GetGroupDto>(group);

        return new ApiResponse<GetGroupDto>
        {
            Code = 200,
            Message = "Group retrieved successfully",
            Data = getGroupDto
        };
    }
}