using Ardalis.Specification;
using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Application.Feature.Students.Models;
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
        try
        {
            var spec = new DbSpecifications<Student>();
            spec.Query.AsNoTracking();

            var students = await _studentRepository.ListAsync(spec, cancellationToken);

            var studentDto = _mapper.Map<List<GetStudentDto>>(students);

            return new ApiResponse<List<GetStudentDto>>
            {
                Code = 1,
                Message = "Students retrieved successfully",
                Data = studentDto
            };
        }
        catch (Exception error)
        {
            return new ApiResponse<List<GetStudentDto>>
            {
                Code = 0,
                Message = $"Error: {error.Message}",
                Data = null
            };
        }
    }

}
