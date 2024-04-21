// <copyright file="ISpriteDrawer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Batching;

using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering.Textures;

public interface ISpriteDrawer
{
    int ProjectionHeight { get; set; }

    int ProjectionWidth { get; set; }

    Matrix4x4 Transform { get; set; }

    void Begin();

    void Draw(ITexture2D texture, Color color, Vector2 origin, Vector2 position, float rotation, Vector2 scale);

    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Begin/End is easier to understand.")]
    void End();
}
