// <copyright file="IndexedModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Models
{
    using System.Collections.Generic;
    using System.Numerics;

    //// TODO: Move Calculate Normals from mesh into a static class.

    public class IndexedModel : IIndexedModel
    {
        public IndexedModel()
        {
            this.Positions = new List<Vector3>();
            this.TextureCoordinates = new List<Vector2>();
            this.Normals = new List<Vector3>();
        }

        public IList<Vector3> Normals { get; }

        public IList<Vector3> Positions { get; }

        public IList<Vector2> TextureCoordinates { get; }
    }
}
