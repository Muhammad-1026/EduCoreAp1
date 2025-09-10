namespace EduCoreApi.Application.Feature.Specialities.Models;

public class GetSpecialityDto
{
    public Guid Id { get; set; }                   
    public string Name { get; set; } = default!;     
    public string Code { get; set; } = default!;     
    public string? Description { get; set; }        

    public Guid DepartmentId { get; set; }          
    public string DepartmentName { get; set; } = default!; 

    public int GroupCount { get; set; }              
    public int StudentCount { get; set; }
}