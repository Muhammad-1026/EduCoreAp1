using EduCoreApi.Application.Feature.Specialitys.Repositories;
using EduCoreApi.Application.Feature.Specialities.Models;
using EduCoreApi.Application.Common.Results;
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

internal sealed class CreateSpecialityCommandHandler : IRequestHandler<CreateSpecialityCommand, ApiResponse<CreateSpecialityDto>>
{
    private readonly ISpecialityRepository _specialityRepository;
    private readonly IMapper _mapper;

    public CreateSpecialityCommandHandler(ISpecialityRepository specialityRepository, IMapper mapper)
    {
        _specialityRepository = specialityRepository;
        _mapper = mapper;
    }

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

        await _specialityRepository.AddAsync(speciality, cancellationToken);
        await _specialityRepository.SaveChangesAsync(cancellationToken);

        var specialityDto = _mapper.Map<CreateSpecialityDto>(speciality);

        return new ApiResponse<CreateSpecialityDto>
        {
            Code = 200,
            Message = "Speciality created successfully",
            Data = specialityDto
        };
    }
}