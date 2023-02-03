// <copyright file="RasterStateDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;

/// <summary>
///   Enumerates the available cull modes that determine which faces will be culled.
/// </summary>
public enum FaceCullMode
{
    /// <summary>
    ///   Specifies front face culling.
    /// </summary>
    Front,

    /// <summary>
    ///   Specifies back face culling.
    /// </summary>
    Back,
}

/// <summary>
///   Enumerates the available rasterization modes that determine how a rasterizer will fill polygons.
/// </summary>
public enum RasterMode
{
    /// <summary>
    ///   Specifies solid-filled polygons.
    /// </summary>
    Solid,

    /// <summary>
    ///   Specifies outlined/wire-frame polygons.
    /// </summary>
    Wireframe,
}

/// <summary>
///   Enumerates the available winding directions used to determine the front face of a primitive.
/// </summary>
public enum WindingDirection
{
    /// <summary>
    ///   Specifies clockwise winding order.
    /// </summary>
    Clockwise,

    /// <summary>
    ///   Specifies counter-clockwise winding order.
    /// </summary>
    CounterClockwise,
}

/// <summary>
///   Represents the rasterization state used in a call to <see cref="IRasterizer.SetRasterState(RasterStateDescription)"/>.
/// </summary>
/// <seealso cref="IEquatable{RasterStateDescription}"/>
public struct RasterStateDescription : IEquatable<RasterStateDescription>
{
    /// <summary>
    ///   The cull mode that determines which faces will be culled.
    /// </summary>
    private FaceCullMode? cullMode;

    /// <summary>
    ///   The fill mode that specifies how the rasterizer will fill polygons.
    /// </summary>
    private RasterMode? fillMode;

    /// <summary>
    ///   The winding direction specifies the direction in which primitives are drawn.
    /// </summary>
    private WindingDirection? windingDirection;

    /// <summary>
    ///   Gets or sets a value indicating whether culling is enabled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if culling is enabled; otherwise, <c>false</c>.
    /// </value>
    public bool CullEnabled { get; set; }

    /// <summary>
    ///   Gets or sets a <see cref="FaceCullMode"/> that represents the cull mode that determines which faces will be culled.
    /// </summary>
    /// <value>
    ///   The cull mode that determines which faces will be culled.
    /// </value>
    public FaceCullMode CullMode
    {
        get { return this.cullMode ?? FaceCullMode.Back; }
        set { this.cullMode = value; }
    }

    /// <summary>
    ///   Gets or sets a <see cref="RasterMode"/> that represents the fill mode that specifies how the rasterizer will fill polygons.
    /// </summary>
    /// <value>
    ///   The fill mode that specifies how the rasterizer will fill polygons.
    /// </value>
    public RasterMode FillMode
    {
        get { return this.fillMode ?? RasterMode.Solid; }
        set { this.fillMode = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether mutlisampling is enabled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if multisampling is enabled; otherwise, <c>false</c>.
    /// </value>
    public bool MultiSamplingEnabled { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether scissoring is enabled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if scissoring is enabled; otherwise, <c>false</c>.
    /// </value>
    public bool ScissorEnabled { get; set; }

    /// <summary>
    ///   Gets or sets a <see cref="WindingDirection"/> that represents the winding direction specifies the direction in which primitives are drawn.
    /// </summary>
    /// <value>
    ///   The winding direction specifies the direction in which primitives are drawn.
    /// </value>
    public WindingDirection WindingDirection
    {
        get { return this.windingDirection ?? WindingDirection.CounterClockwise; }
        set { this.windingDirection = value; }
    }

    /// <summary>
    ///   Implements the operator !=.
    /// </summary>
    /// <param name="left">
    ///   Specifies a <see cref="RasterStateDescription"/> that represents the left operand.
    /// </param>
    /// <param name="right">
    ///   Specifies a <see cref="RasterStateDescription"/> that represents the right operand.
    /// </param>
    /// <returns>
    ///   The result of the operator.
    /// </returns>
    public static bool operator !=(RasterStateDescription left, RasterStateDescription right)
    {
        return !(left == right);
    }

    /// <summary>
    ///   Implements the operator ==.
    /// </summary>
    /// <param name="left">
    ///   Specifies a <see cref="RasterStateDescription"/> that represents the left operand.
    /// </param>
    /// <param name="right">
    ///   Specifies a <see cref="RasterStateDescription"/> that represents the right operand.
    /// </param>
    /// <returns>
    ///   The result of the operator.
    /// </returns>
    public static bool operator ==(RasterStateDescription left, RasterStateDescription right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///   Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">
    ///   An object to compare with this object.
    /// </param>
    /// <returns>
    ///   <see langword="true"/> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(RasterStateDescription other)
    {
        return this.CullEnabled == other.CullEnabled &&
               this.CullMode == other.CullMode &&
               this.FillMode == other.FillMode &&
               this.ScissorEnabled == other.ScissorEnabled &&
               this.WindingDirection == other.WindingDirection &&
               this.MultiSamplingEnabled == other.MultiSamplingEnabled;
    }

    /// <summary>
    ///   Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">
    ///   The object to compare with the current instance.
    /// </param>
    /// <returns>
    ///   <see langword="true"/> if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj)
    {
        return obj is RasterStateDescription description && this.Equals(description);
    }

    /// <summary>
    ///   Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    ///   A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public override int GetHashCode()
    {
        const int accumulator = 17;

        return (this.CullEnabled.GetHashCode() * accumulator) +
               (this.CullMode.GetHashCode() * accumulator) +
               (this.FillMode.GetHashCode() * accumulator) +
               (this.ScissorEnabled.GetHashCode() * accumulator) +
               (this.WindingDirection.GetHashCode() * accumulator) +
               (this.MultiSamplingEnabled.GetHashCode() * accumulator);
    }
}
