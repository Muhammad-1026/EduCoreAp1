using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Subjects.Models;
using EduCoreApi.Application.Feature.Subjects.Repositories;
using EduCoreApi.Application.Feature.Subjects.Specifications;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Subjects.Queries;

public sealed record GetSubjectById(Guid SubjectId) : IRequest<ApiResponse<GetSubjectDto>>;

public sealed class GetSubjectByIdValidator : AbstractValidator<GetSubjectById>
{
    public GetSubjectByIdValidator()
    {
        RuleFor(x => x.SubjectId)
            .GreaterThan(Guid.Empty);
    }
}

internal sealed class GetSubjectByIdHendler : IRequestHandler<GetSubjectById, ApiResponse<GetSubjectDto>>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;

    public GetSubjectByIdHendler(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<GetSubjectDto>> Handle(GetSubjectById request, CancellationToken cancellationToken)
    {
        var spec = new SubjectByIdSpec(request.SubjectId, asNoTracking: true);

        var group = await _subjectRepository.FirstOrDefaultAsync(spec, cancellationToken);


        if (group == null)
        {
            return new ApiResponse<GetSubjectDto>
            {
                Code = 404,
                Message = "Group no found",
                Data = null
            };
        }

        var getGroupDto = _mapper.Map<GetSubjectDto>(group);

        return new ApiResponse<GetSubjectDto>
        {
            Code = 200,
            Message = "Group retrieved successfully",
            Data = getGroupDto
        };
    }
}