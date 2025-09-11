using EduCoreApi.Application.Feature.Specialitys.Specifications;
using EduCoreApi.Application.Feature.Specialitys.Repositories;
using EduCoreApi.Application.Common.Results;
using FluentValidation;
using MediatR;

namespace EduCoreApi.Application.Feature.Specialities.Commands;

public sealed record DeleteSpecialityCommand(Guid SpecialityId) : IRequest<ApiResponse>;

public sealed class DeleteSpecialityCommandValidator : AbstractValidator<DeleteSpecialityCommand>
{
    public DeleteSpecialityCommandValidator()
    {
        RuleFor(x => x.SpecialityId).NotEmpty();
    }
}

internal sealed class DeleteSpecialityCommandHandler : IRequestHandler<DeleteSpecialityCommand, ApiResponse>
{
    private readonly ISpecialityRepository _specialityRepository;

    public DeleteSpecialityCommandHandler(ISpecialityRepository specialityRepository)
    {
        _specialityRepository = specialityRepository;
    }

    public async Task<ApiResponse> Handle(DeleteSpecialityCommand request, CancellationToken cancellationToken)
    {
        var speciality = await _specialityRepository.FirstOrDefaultAsync(new SpecialityByIdSpec(request.SpecialityId), cancellationToken);

        if (speciality is null)
        {
            return new ApiResponse
            {
                Code = 404,
                Message = "Speciality not found"
            };
        }

        await _specialityRepository.DeleteAsync(speciality, cancellationToken);
        await _specialityRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse
        {
            Code = 200,
            Message = "Speciality deleted successfully",
        };
    }
}