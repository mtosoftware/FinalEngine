// <copyright file="TestComponent.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Core;

/// <summary>
/// Tetst compoinetn.
/// </summary>
/// <seealso cref="FinalEngine.ECS.IEntityComponent" />
public sealed class CharacterComponent : IEntityComponent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterComponent"/> class.
    /// </summary>
    public CharacterComponent()
    {
        this.Name = "Character";
        this.Level = 1;
        this.XP = 0;
        this.IsAlive = true;
        this.CanAttack = false;
    }

    public bool CanAttack { get; set; }

    public bool IsAlive { get; set; }

    public int Level { get; set; }

    public string? Name { get; set; }

    public int XP { get; set; }
}
