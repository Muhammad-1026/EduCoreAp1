using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Specialities.Models;
using EduCoreApi.Application.Feature.Specialitys.Specifications;
using Ardalis.Specification;
using FluentValidation;
using MapsterMapper;
using MediatR;
using EduCoreApi.Application.Common.Repositories;

namespace EduCoreApi.Application.Feature.Specialitys.Queries;

public sealed record GetSpecialityById(Guid SpecialityId) : IRequest<ApiResponse<GetSpecialityDto>>;

public sealed class GetSpecialityByIdValidator : AbstractValidator<GetSpecialityById>
{
    public GetSpecialityByIdValidator()
    {
        RuleFor(x => x.SpecialityId).GreaterThan(Guid.Empty);
    }
}

internal sealed class GetSpecialityByIdHendler(ISpecialityRepository specialityRepository, IMapper mapper) : IRequestHandler<GetSpecialityById, ApiResponse<GetSpecialityDto>>
{
    public async Task<ApiResponse<GetSpecialityDto>> Handle(GetSpecialityById request, CancellationToken cancellationToken)
    {
        var spec = new SpecialityByIdSpec(request.SpecialityId, asNoTracking: true);
        spec
            .Query
            .AsNoTracking()
            .Include(s => s.Department);

        var speciality = await specialityRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (speciality == null)
        {
            return new ApiResponse<GetSpecialityDto>
            {
                Code = 404,
                Message = "Speciality not found",
                Data = null
            };
        }

        var specialityDto = mapper.Map<GetSpecialityDto>(speciality);

        return new ApiResponse<GetSpecialityDto>
        {
            Code = 200,
            Message = "Speciality retrieved successfully",
            Data = specialityDto
        };
    }
}
