using EduCoreApi.Application.Feature.Specialities.Models;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Domain.Models;
using Ardalis.Specification;
using MapsterMapper;
using MediatR;
using EduCoreApi.Application.Common.Repositories;

namespace EduCoreApi.Application.Feature.Specialitys.Queries;

public sealed record GetSpecialities : IRequest<ApiResponse<List<GetSpecialityDto>>>;

internal sealed class GetSpecialitiesHendler : IRequestHandler<GetSpecialities, ApiResponse<List<GetSpecialityDto>>>
{
    private readonly ISpecialityRepository _specialityRepository;
    private readonly IMapper _mapper;

    public GetSpecialitiesHendler(ISpecialityRepository specialityRepository, IMapper mapper)
    {
        _specialityRepository = specialityRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<GetSpecialityDto>>> Handle(GetSpecialities request, CancellationToken cancellationToken)
    {
        var spec = new DbSpecifications<Speciality>();
        spec
            .Query
            .AsNoTracking()
            .Include(s => s.Department);

        var specialities = await _specialityRepository.ListAsync(spec, cancellationToken);

        if (specialities == null)
        {
            return new ApiResponse<List<GetSpecialityDto>>
            {
                Code = 404,
                Message = "Specialities no found",
                Data = null
            };
        }

        var specialityDto =  _mapper.Map<List<GetSpecialityDto>>(specialities);

        return new ApiResponse<List<GetSpecialityDto>>
        {
            Code = 200,
            Message = "Specialities retrieved successfully",
            Data = specialityDto
        };
    }
}
