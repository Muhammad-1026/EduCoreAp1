﻿namespace EduCoreApi.Application.Feature.Subjects.Models;

public class CreateSubjectDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
