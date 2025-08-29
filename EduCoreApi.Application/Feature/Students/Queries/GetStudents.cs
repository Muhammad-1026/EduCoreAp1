using Ardalis.Specification;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Application.Feature.Students.Models;
using EduCoreApi.Application.Feature.Students.Repositories;
using EduCoreApi.Domain.Models;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Students.Queries;

public sealed record GetStudents : IRequest<List<GetStudentDto>>;

internal sealed class GetStudentsHendler : IRequestHandler<GetStudents, List<GetStudentDto>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public GetStudentsHendler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    public async Task<List<GetStudentDto>> Handle(GetStudents request, CancellationToken cancellationToken)
    {
        var spec = new DbSpecifications<Student>();
        spec.Query.AsNoTracking();

        var student = await _studentRepository.ListAsync(spec, cancellationToken);

        return _mapper.Map<List<GetStudentDto>>(student);
    }
}
