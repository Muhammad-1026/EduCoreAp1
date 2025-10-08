using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Subjects.Models;
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

internal sealed class GetSubjectByIdHendler(ISubjectRepository subjectRepository, IMapper mapper) : IRequestHandler<GetSubjectById, ApiResponse<GetSubjectDto>>
{
    public async Task<ApiResponse<GetSubjectDto>> Handle(GetSubjectById request, CancellationToken cancellationToken)
    {
        var spec = new SubjectByIdSpec(request.SubjectId, asNoTracking: true);

        var group = await subjectRepository.FirstOrDefaultAsync(spec, cancellationToken);


        if (group == null)
        {
            return new ApiResponse<GetSubjectDto>
            {
                Code = 404,
                Message = "Subject no found",
                Data = null
            };
        }

        var getGroupDto = mapper.Map<GetSubjectDto>(group);

        return new ApiResponse<GetSubjectDto>
        {
            Code = 200,
            Message = "Subject retrieved successfully",
            Data = getGroupDto
        };
    }
}