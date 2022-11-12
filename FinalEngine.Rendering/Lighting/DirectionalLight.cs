// <copyright file="DirectionalLight.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Lighting
{
    using System.Numerics;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Resources;

    public class DirectionalLight : LightBase
    {
        private static readonly IShaderProgram DefaultShaderProgram = ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\forward-directional.fesp");

        public Vector3 Direction { get; set; }

        public override IShaderProgram ShaderProgram
        {
            get { return DefaultShaderProgram; }
        }
    }
}
