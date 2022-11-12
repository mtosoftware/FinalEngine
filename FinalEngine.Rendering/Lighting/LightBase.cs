// <copyright file="LightBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Lighting
{
    using System.Numerics;
    using FinalEngine.Rendering.Pipeline;

    public abstract class LightBase
    {
        public Vector3 AmbientColor { get; set; }

        public Vector3 DiffuseColor { get; set; }

        public abstract IShaderProgram ShaderProgram { get; }

        public Vector3 SpecularColor { get; set; }
    }
}
