// <copyright file="ModelComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Rendering
{
    using FinalEngine.Rendering;

    public class ModelComponent : IComponent
    {
        public IMaterial? Material { get; set; }

        public IMesh? Mesh { get; set; }
    }
}
