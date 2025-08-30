using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Feature.Students.Models;
using EduCoreApi.Application.Feature.Students.Specifications;
using EduCoreApi.Shared.Exeptions;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Students.Queries;

public sealed record GetStudentByIdQuery(Guid StudentId) : IRequest<GetStudentDto>;

public sealed class GetStudentByIdValidator : AbstractValidator<GetStudentByIdQuery>
{
    public GetStudentByIdValidator()
    {
        RuleFor(x => x.StudentId).GreaterThan(Guid.Empty);
    }
}

internal sealed class GetStudentByIdHendler : IRequestHandler<GetStudentByIdQuery, GetStudentDto>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public GetStudentByIdHendler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    public async Task<GetStudentDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new StudentByIdSpec(request.StudentId, asNoTracking: true);

        var student = await _studentRepository.FirstOrDefaultAsync(spec, cancellationToken) 
            ?? throw new ResourceNotFoundException(StudentError.NotFound);

        return _mapper.Map<GetStudentDto>(student);
    }
}