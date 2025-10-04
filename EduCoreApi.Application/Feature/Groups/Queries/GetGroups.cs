using Ardalis.Specification;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Application.Feature.Groups.Models;
using EduCoreApi.Application.Feature.Groups.Repositories;
using EduCoreApi.Domain.Models;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Groups.Queries;

public sealed record GetGroups : IRequest<ApiResponse<List<GetGroupDto>>>;

internal sealed class GetGroupsHendler : IRequestHandler<GetGroups, ApiResponse<List<GetGroupDto>>>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public GetGroupsHendler(IGroupRepository groupRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<GetGroupDto>>> Handle(GetGroups request, CancellationToken cancellationToken)
    {
        var spec = new DbSpecifications<Group>();

        spec
            .Query
            .AsNoTracking()
            .Include(s => s.Speciality);

        var specialities = await _groupRepository.ListAsync(spec, cancellationToken);

        if (specialities == null)
        {
            return new ApiResponse<List<GetGroupDto>>
            {
                Code = 404,
                Message = "Specialities no found",
                Data = null
            };
        }

        var specialityDto = _mapper.Map<List<GetGroupDto>>(specialities);

        return new ApiResponse<List<GetGroupDto>>
        {
            Code = 200,
            Message = "Specialities retrieved successfully",
            Data = specialityDto
        };
    }
}