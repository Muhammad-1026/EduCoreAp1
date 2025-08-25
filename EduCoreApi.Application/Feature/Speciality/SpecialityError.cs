using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Speciality;

public class SpecialityError
{
    public static readonly Error NotFound = new(
        "Speciality.NotFound",
        "Специальность не найдена"
    );
}
