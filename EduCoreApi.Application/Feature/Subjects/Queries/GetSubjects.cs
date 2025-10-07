using Ardalis.Specification;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Application.Feature.Subjects.Models;
using EduCoreApi.Domain.Models;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Subjects.Queries;

public sealed record GetSubjects : IRequest<ApiResponse<List<GetSubjectDto>>>;

internal sealed class GetSubjectsHendler : IRequestHandler<GetSubjects, ApiResponse<List<GetSubjectDto>>>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;

    public GetSubjectsHendler(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<GetSubjectDto>>> Handle(GetSubjects request, CancellationToken cancellationToken)
    {
        var spec = new DbSpecifications<Subject>();

        spec
            .Query
            .AsNoTracking();

        var specialities = await _subjectRepository.ListAsync(spec, cancellationToken);

        if (specialities == null)
        {
            return new ApiResponse<List<GetSubjectDto>>
            {
                Code = 404,
                Message = "Specialities no found",
                Data = null
            };
        }

        var specialityDto = _mapper.Map<List<GetSubjectDto>>(specialities);

        return new ApiResponse<List<GetSubjectDto>>
        {
            Code = 200,
            Message = "Specialities retrieved successfully",
            Data = specialityDto
        };
    }
}