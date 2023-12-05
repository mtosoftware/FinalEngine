// <copyright file="IEntityWorld.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS;

using System;

public interface IEntityWorld : IEntitySystemsProcessor
{
    void AddEntity(Entity entity);

    void AddSystem(EntitySystemBase system);

    void RemoveEntity(Entity entity);

    void RemoveSystem(Type type);
}
