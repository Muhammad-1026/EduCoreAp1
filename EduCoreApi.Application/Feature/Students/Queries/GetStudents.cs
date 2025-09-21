using Ardalis.Specification;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Application.Feature.Students.Models;
using EduCoreApi.Application.Feature.Students.Repositories;
using EduCoreApi.Domain.Models;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Students.Queries;

public sealed record GetStudents : IRequest<ApiResponse<List<GetStudentDto>>>;

internal sealed class GetStudentsHendler : IRequestHandler<GetStudents, ApiResponse<List<GetStudentDto>>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public GetStudentsHendler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<GetStudentDto>>> Handle(GetStudents request, CancellationToken cancellationToken)
    {
        var spec = new DbSpecifications<Student>();
        spec.Query.AsNoTracking();

        var students = await _studentRepository.ListAsync(spec, cancellationToken);

        if (students == null)
        {
            return new ApiResponse<List<GetStudentDto>>
            {
                Code = 404,
                Message = "No students found",
                Data = null
            };
        }

        var studentDto = _mapper.Map<List<GetStudentDto>>(students);

        return new ApiResponse<List<GetStudentDto>>
        {
            Code = 200,
            Message = "Students retrieved successfully",
            Data = studentDto
        };
    }
}