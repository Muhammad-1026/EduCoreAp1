using EduCoreApi.Application.Feature.Specialitys.Specifications;
using EduCoreApi.Application.Common.Results;
using FluentValidation;
using MediatR;
using EduCoreApi.Application.Common.Repositories;

namespace EduCoreApi.Application.Feature.Specialities.Commands;

public sealed record DeleteSpecialityCommand(Guid SpecialityId) : IRequest<ApiResponse>;

public sealed class DeleteSpecialityCommandValidator : AbstractValidator<DeleteSpecialityCommand>
{
    public DeleteSpecialityCommandValidator()
    {
        RuleFor(x => x.SpecialityId).NotEmpty();
    }
}

internal sealed class DeleteSpecialityCommandHandler(ISpecialityRepository specialityRepository) : IRequestHandler<DeleteSpecialityCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteSpecialityCommand request, CancellationToken cancellationToken)
    {
        var speciality = await specialityRepository.FirstOrDefaultAsync(new SpecialityByIdSpec(request.SpecialityId), cancellationToken);

        if (speciality is null)
        {
            return new ApiResponse
            {
                Code = 404,
                Message = "Speciality not found"
            };
        }

        await specialityRepository.DeleteAsync(speciality, cancellationToken);
        await specialityRepository.SaveChangesAsync(cancellationToken);

        return new ApiResponse
        {
            Code = 200,
            Message = "Speciality deleted successfully",
        };
    }
}