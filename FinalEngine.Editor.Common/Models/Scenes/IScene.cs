// <copyright file="IScene.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models.Scenes;

using System;
using System.Collections.Generic;
using FinalEngine.ECS;

public interface IScene
{
    IReadOnlyCollection<Entity> Entities { get; }

    void AddEntity(string tag, Guid uniqueID);

    void AddSystem<TSystem>()
        where TSystem : EntitySystemBase;

    void RemoveEntity(Guid uniqueIdentifier);

    void RemoveSystem<TSystem>()
        where TSystem : EntitySystemBase;

    void Render();
}
