using EduCoreApi.Application.Feature.Teachers.Specifications;
using EduCoreApi.Application.Feature.Teachers.Models;
using EduCoreApi.Application.Common.Results;
using FluentValidation;
using MapsterMapper;
using MediatR;
using EduCoreApi.Application.Common.Repositories;

namespace EduCoreApi.Application.Feature.Teachers.Queries;

public sealed record GetTeacherById(Guid TeacherId) : IRequest<ApiResponse<GetTeacherDto>>;

public sealed class GetTeacherByIdValidator : AbstractValidator<GetTeacherById>
{
    public GetTeacherByIdValidator()
    {
        RuleFor(x => x.TeacherId).GreaterThan(Guid.Empty);
    }
}

internal sealed class GetByIdTeacherHandler(ITeacherRepository teacherRepository, IMapper mapper) : IRequestHandler<GetTeacherById, ApiResponse<GetTeacherDto>>
{
    public async Task<ApiResponse<GetTeacherDto>> Handle(GetTeacherById request, CancellationToken cancellationToken)
    {
        var spec = new TeacherByIdSpec(request.TeacherId, asNoTracking: true);
        var teacher = await teacherRepository.FirstOrDefaultAsync(spec, cancellationToken);

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
            Data = mapper.Map<GetTeacherDto>(teacher)
        };
    }
}