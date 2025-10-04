using Ardalis.Specification;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Domain.Models;

namespace EduCoreApi.Application.Feature.Groups.Specifications;

public class GroupByIdSpec : DbSpecifications<Group>
{
    public Guid GroupId { get; }

    public GroupByIdSpec(Guid groupId, bool asNoTracking = false)
    {
        GroupId = groupId;

        if (asNoTracking)
            Query.AsNoTracking();

        Query.Where(x => x.Id == GroupId);
        Query.Include(speciality => speciality.Speciality);
    }
}