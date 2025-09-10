using Ardalis.Specification;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Application.Feature.Specialities.Models;
using EduCoreApi.Application.Feature.Specialitys.Repositories;
using MapsterMapper;
using MediatR;

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

    public Task<ApiResponse<List<GetSpecialityDto>>> Handle(GetSpecialities request, CancellationToken cancellationToken)
    {
        var spec = new DbSpecifications<Domain.Models.Speciality>();
        spec.Query.AsNoTracking();

        var specialities = _specialityRepository.ListAsync(spec, cancellationToken);

        if (specialities == null)
        {
            return Task.FromResult(new ApiResponse<List<GetSpecialityDto>>
            {
                Code = 404,
                Message = "Specialities no found",
                Data = null
            });
        }

        var specialityDto = _mapper.Map<List<GetSpecialityDto>>(specialities.Result);

        return Task.FromResult(new ApiResponse<List<GetSpecialityDto>>
        {
            Code = 200,
            Message = "Specialities retrieved successfully",
            Data = specialityDto
        });

    }
}
