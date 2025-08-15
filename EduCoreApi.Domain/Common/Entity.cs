﻿namespace EduCoreApi.Domain.Common;

public class Entity
{
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? UpdatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}