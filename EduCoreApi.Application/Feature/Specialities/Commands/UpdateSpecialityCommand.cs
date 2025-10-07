using EduCoreApi.Application.Common.Repositories;
using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Specialitys.Specifications;
using EduCoreApi.Domain.Models;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Specialities.Commands;

public sealed record UpdateSpecialityCommand(Guid SpecialityId, string Name, string Code, string? Description) : IRequest<ApiResponse<Guid>>;

public sealed class UpdateSpecialityCommandValidator : AbstractValidator<UpdateSpecialityCommand>
{
    public UpdateSpecialityCommandValidator()
    {
        RuleFor(x => x.SpecialityId)
            .NotEmpty();
        RuleFor(x => x.Name)
            .NotEmpty();
        RuleFor(x => x.Code)
            .NotEmpty();
        RuleFor(x => x.Description)
            .NotEmpty();    
    }
}

internal sealed class UpdateSpecialityCommandHandler : IRequestHandler<UpdateSpecialityCommand, ApiResponse<Guid>>
{
    private readonly ISpecialityRepository _specialityRepository;
    public UpdateSpecialityCommandHandler(ISpecialityRepository specialityRepository)
    {
        _specialityRepository = specialityRepository;
    }

    public async Task<ApiResponse<Guid>> Handle(UpdateSpecialityCommand request, CancellationToken cancellationToken)
    {
        var speciality = await _specialityRepository.FirstOrDefaultAsync(new SpecialityByIdSpec(request.SpecialityId), cancellationToken);

        if (speciality is null)
        {
            return new ApiResponse<Guid>
            {
                Code = 404,
                Message = "Speciality not found"
            };
        }

        speciality.SetName(request.Name);
        speciality.SetCode(request.Code);

        if (!string.IsNullOrWhiteSpace(request.Description))
            speciality.SetDescription(request.Description);

        await _specialityRepository.UpdateAsync(speciality, cancellationToken);
        await _specialityRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Guid>
        {
            Code = 200,
            Message = "Speciality updated successfully",
            Data = speciality.Id,
        };
    }
}