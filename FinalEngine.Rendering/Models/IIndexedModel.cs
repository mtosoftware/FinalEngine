// <copyright file="IIndexedModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Models
{
    using System.Collections.Generic;
    using System.Numerics;

    public interface IIndexedModel
    {
        IList<Vector3> Normals { get; }

        IList<Vector3> Positions { get; }

        IList<Vector2> TextureCoordinates { get; }
    }
}
