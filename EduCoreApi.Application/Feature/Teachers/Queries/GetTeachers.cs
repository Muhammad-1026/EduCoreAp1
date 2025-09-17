using EduCoreApi.Application.Feature.Teachers.Repositories;
using EduCoreApi.Application.Feature.Teachers.Models;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Domain.Models;
using Ardalis.Specification;
using MapsterMapper;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EduCoreApi.Application.Feature.Teachers.Queries;

public sealed record GetTeachers : IRequest<ApiResponse<List<GetTeacherDto>>>;

internal sealed class GetTeachersHandler : IRequestHandler<GetTeachers, ApiResponse<List<GetTeacherDto>>>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;

    public GetTeachersHandler(ITeacherRepository teacherRepository, IMapper mapper)
    {
        _teacherRepository = teacherRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<GetTeacherDto>>> Handle(GetTeachers request, CancellationToken cancellationToken)
    {
        var spec = new DbSpecifications<Teacher>();

        spec.Query
            .Where(t => t.IsActive)
            .Include(s => s.Department)
            .AsNoTracking();

        var teachers = await _teacherRepository.ListAsync(spec, cancellationToken);

        if (teachers == null || !teachers.Any())
        {
            return new ApiResponse<List<GetTeacherDto>>
            {
                Code = 404,
                Message = "Teachers not found",
            };
        }

        var teacherDto = _mapper.Map<List<GetTeacherDto>>(teachers);

        return new ApiResponse<List<GetTeacherDto>>
        {
            Code = 200,
            Message = "Teachers retrieved successfully",
            Data = teacherDto
        };
    }
}