// <copyright file="IMaterial.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using FinalEngine.Rendering.Textures;

    public interface IMaterial
    {
        ITexture2D? DiffuseTexture { get; set; }

        void Bind(IPipeline pipeline);
    }
}
