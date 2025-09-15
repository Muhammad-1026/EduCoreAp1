using EduCoreApi.Application.Feature.Teachers.Specifications;
using EduCoreApi.Application.Feature.Teachers.Repositories;
using EduCoreApi.Application.Feature.Teachers.Models;
using EduCoreApi.Application.Common.Results;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Teachers.Queries;

public sealed record GetTeacherById(Guid TeacherId) : IRequest<ApiResponse<GetTeacherDto>>;

public sealed class GetTeacherByIdValidator : AbstractValidator<GetTeacherById>
{
    public GetTeacherByIdValidator()
    {
        RuleFor(x => x.TeacherId).GreaterThan(Guid.Empty);
    }
}

internal sealed class GetByIdTeacherHandler : IRequestHandler<GetTeacherById, ApiResponse<GetTeacherDto>>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;

    public GetByIdTeacherHandler(ITeacherRepository teacherRepository, IMapper mapper)
    {
        _teacherRepository = teacherRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<GetTeacherDto>> Handle(GetTeacherById request, CancellationToken cancellationToken)
    {
        var spec = new TeacherByIdSpec(request.TeacherId, asNoTracking: true);
        var teacher = await _teacherRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (teacher == null)
            return new ApiResponse<GetTeacherDto>
            {
                Code = 404,
                Message = "Teacher not found"
            };
        
        return new ApiResponse<GetTeacherDto>
        {
            Code = 200,
            Message = "Teacher retrieved successfully",
            Data = _mapper.Map<GetTeacherDto>(teacher)
        };
    }
}