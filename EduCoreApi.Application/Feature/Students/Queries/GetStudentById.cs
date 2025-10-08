using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Students.Models;
using EduCoreApi.Application.Feature.Students.Repositories;
using EduCoreApi.Application.Feature.Students.Specifications;
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

internal sealed class GetStudentByIdHendler(IStudentRepository studentRepository, IMapper mapper) : IRequestHandler<GetStudentById, ApiResponse<GetStudentDto>>
{
    public async Task<ApiResponse<GetStudentDto>> Handle(GetStudentById request, CancellationToken cancellationToken)
    {
        var spec = new StudentByIdSpec(request.StudentId, asNoTracking: true);

        var student = await studentRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (student == null)
        {
            return new ApiResponse<GetStudentDto>
            {
                Code = 404,
                Message = "Student not found",
                Data = null
            };
        }

        var speciality = mapper.Map<GetStudentDto>(student);

        return new ApiResponse<GetStudentDto>
        {
            Code = 200,
            Message = "Student retrieved successfully",
            Data = speciality
        };
    }
}