// <copyright file="IEntityTemplate.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Templates
{
    using FinalEngine.ECS;

    public interface IEntityTemplate
    {
        Entity CreateEntity();
    }
}