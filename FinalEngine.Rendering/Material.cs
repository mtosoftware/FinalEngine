// <copyright file="Material.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;
    using FinalEngine.Rendering.Textures;
    using FinalEngine.Resources;

    public class Material : IMaterial
    {
        private const int DiffuseTextureSlot = 0;

        private static readonly ITexture2D DefaultDiffuseTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_diffuse.png");

        private static IMaterial? @default;

        public static IMaterial Default
        {
            get { return @default ??= new Material(); }
        }

        public ITexture2D? DiffuseTexture { get; set; }

        public void Bind(IPipeline pipeline)
        {
            if (pipeline == null)
            {
                throw new ArgumentNullException(nameof(pipeline));
            }

            if (this.DiffuseTexture == null)
            {
                this.DiffuseTexture = DefaultDiffuseTexture;
            }

            pipeline.SetUniform("u_material.diffuseTexture", DiffuseTextureSlot);
            pipeline.SetTexture(this.DiffuseTexture, DiffuseTextureSlot);
        }
    }
}
