using Ardalis.Specification;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Application.Feature.Groups.Models;
using EduCoreApi.Domain.Models;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Groups.Queries;

public sealed record GetGroups : IRequest<ApiResponse<List<GetGroupDto>>>;

internal sealed class GetGroupsHendler(IGroupRepository groupRepository, IMapper mapper) : IRequestHandler<GetGroups, ApiResponse<List<GetGroupDto>>>
{
    public async Task<ApiResponse<List<GetGroupDto>>> Handle(GetGroups request, CancellationToken cancellationToken)
    {
        var spec = new DbSpecifications<Group>();

        spec
            .Query
            .AsNoTracking()
            .Include(s => s.Speciality);

        var specialities = await groupRepository.ListAsync(spec, cancellationToken);

        if (specialities == null)
        {
            return new ApiResponse<List<GetGroupDto>>
            {
                Code = 404,
                Message = "Specialities no found",
                Data = null
            };
        }

        var specialityDto = mapper.Map<List<GetGroupDto>>(specialities);

        return new ApiResponse<List<GetGroupDto>>
        {
            Code = 200,
            Message = "Specialities retrieved successfully",
            Data = specialityDto
        };
    }
}