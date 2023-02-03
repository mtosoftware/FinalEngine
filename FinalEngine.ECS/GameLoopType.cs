// <copyright file="GameLoopType.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS;

/// <summary>
///   Enumerates the available execution types for an entity system.
/// </summary>
public enum GameLoopType
{
    /// <summary>
    ///   The system will be processed per update.
    /// </summary>
    Update,

    /// <summary>
    ///   The system will be processed per render.
    /// </summary>
    Render,
}
