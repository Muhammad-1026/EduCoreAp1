using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Students.Models;
using EduCoreApi.Application.Feature.Students.Specifications;
using EduCoreApi.Shared.Exeptions;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Students.Queries;

public sealed record GetStudentById(Guid StudentId) : IRequest<ApiResponse<GetStudentDto>>;

public sealed class GetStudentByIdValidator : AbstractValidator<GetStudentById>
{
    public GetStudentByIdValidator()
    {
        RuleFor(x => x.StudentId).GreaterThan(Guid.Empty);
    }
}

internal sealed class GetStudentByIdHendler : IRequestHandler<GetStudentById, ApiResponse<GetStudentDto>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public GetStudentByIdHendler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<GetStudentDto>> Handle(GetStudentById request, CancellationToken cancellationToken)
    {
        var spec = new StudentByIdSpec(request.StudentId, asNoTracking: true);

        var student = await _studentRepository.FirstOrDefaultAsync(spec, cancellationToken) 
            ?? throw new ResourceNotFoundException(StudentError.NotFound);

        return _mapper.Map<ApiResponse<GetStudentDto>>(student);
    }
}