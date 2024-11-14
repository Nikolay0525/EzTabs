﻿using System.ComponentModel.DataAnnotations;


namespace EzTabs.Data.Domain.BaseModels;

public abstract class Entity
{
    [Key]
    public Guid Id { get; private set; } = Guid.NewGuid();
}
