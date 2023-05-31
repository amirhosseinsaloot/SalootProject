﻿namespace DomainDrivenDesign.Domain.Interfaces.Entities;

/// <summary>
/// Provides a DateTime named CreatedOn only for entities.
/// </summary>
public interface ICreatedOn : IEntity
{
    public DateTime CreatedOn { get; }
}
