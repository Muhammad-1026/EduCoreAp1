using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Specialities.Models;
using EduCoreApi.Domain.Models;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace EduCoreApi.Application.Feature.Specialitys.Commands;

public sealed record CreateSpecialityCommand(string Name, string Code, Guid DepartmentId, string? Description) : IRequest<ApiResponse<CreateSpecialityDto>>;

public sealed class CreateSpecialityCommandValidator : AbstractValidator<CreateSpecialityCommand>
{
    public CreateSpecialityCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        RuleFor(x => x.Code)
            .NotEmpty();
        RuleFor(x => x.DepartmentId)
            .NotEmpty();
        RuleFor(x => x.Description)
            .NotEmpty();
    }
}

internal sealed class CreateSpecialityCommandHandler(ISpecialityRepository specialityRepository, IMapper mapper) : IRequestHandler<CreateSpecialityCommand, ApiResponse<CreateSpecialityDto>>
{
    public async Task<ApiResponse<CreateSpecialityDto>> Handle(CreateSpecialityCommand request, CancellationToken cancellationToken)
    {
        var speciality = new Speciality(
            request.Name,
            request.Code,
            request.DepartmentId,
            // createdBy TODO: replace with actual user id
            Guid.Empty
        );

        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            speciality.SetDescription(request.Description);
        }

        await specialityRepository.AddAsync(speciality, cancellationToken);
        await specialityRepository.SaveChangesAsync(cancellationToken);

        var specialityDto = mapper.Map<CreateSpecialityDto>(speciality);

        return new ApiResponse<CreateSpecialityDto>
        {
            Code = 200,
            Message = "Speciality created successfully",
            Data = specialityDto
        };
    }
}