using Ardalis.Specification;
using EduCoreApi.Application.Common.Specifications;

namespace EduCoreApi.Application.Feature.Specialitys.Specifications;

public class SpecialityByIdSpec : DbSpecifications<Domain.Models.Speciality>
{
    public Guid SpecialityId { get; }
    public SpecialityByIdSpec(Guid specialityId, bool asNoTracking = false)
    {
        SpecialityId = specialityId;

        if (asNoTracking)
            Query.AsNoTracking();

        Query.Where(speciality => speciality.Id == SpecialityId);
        Query.Include(speciality => speciality.Department);
    }
}