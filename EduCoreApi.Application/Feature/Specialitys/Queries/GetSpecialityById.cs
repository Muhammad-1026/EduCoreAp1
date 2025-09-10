using EduCoreApi.Application.Feature.Specialitys.Specifications;
using EduCoreApi.Application.Feature.Specialitys.Repositories;
using EduCoreApi.Application.Feature.Speciality.Models;
using EduCoreApi.Application.Common.Results;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Specialitys.Queries;

public sealed record GetSpecialityById(Guid SpecialityId) : IRequest<ApiResponse<GetSpecialityDto>>;

public sealed class GetSpecialityByIdValidator : AbstractValidator<GetSpecialityById>
{
    public GetSpecialityByIdValidator()
    {
        RuleFor(x => x.SpecialityId).GreaterThan(Guid.Empty);
    }
}

internal sealed class GetSpecialityByIdHendler : IRequestHandler<GetSpecialityById, ApiResponse<GetSpecialityDto>>
{
    private readonly ISpecialityRepository _specialityRepository;
    private readonly IMapper _mapper;
    public GetSpecialityByIdHendler(ISpecialityRepository specialityRepository, IMapper mapper)
    {
        _specialityRepository = specialityRepository;
        _mapper = mapper;
    }
    public async Task<ApiResponse<GetSpecialityDto>> Handle(GetSpecialityById request, CancellationToken cancellationToken)
    {
        var spec = new SpecialityByIdSpec(request.SpecialityId, asNoTracking: true);
        var speciality = await _specialityRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (speciality == null)
        {
            return new ApiResponse<GetSpecialityDto>
            {
                Code = 404,
                Message = "Speciality not found",
                Data = null
            };
        }

        var a = _mapper.Map<ApiResponse<GetSpecialityDto>>(speciality);

        return new ApiResponse<GetSpecialityDto>
        {
            Code = 200,
            Message = "Speciality retrieved successfully",
            Data = a.Data
        };
    }
}
