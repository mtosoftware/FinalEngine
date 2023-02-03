// <copyright file="BlendStateDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Drawing;

/// <summary>
///   Specifies the Enumerates the available blend equation modes that determine how the source and destination factors are combined in a blend operation.
/// </summary>
public enum BlendEquationMode
{
    /// <summary>
    ///   Specifies the source and destination are added.
    /// </summary>
    Add,

    /// <summary>
    ///   Specifies the destination is subtracted from source.
    /// </summary>
    Subtract,

    /// <summary>
    ///   Specifies the source is subtracted from destination.
    /// </summary>
    ReverseSubtract,

    /// <summary>
    ///   Specifies the minimum of source and destination is selected.
    /// </summary>
    Min,

    /// <summary>
    ///   Specifies the maximum of source and destination is selected.
    /// </summary>
    Max,
}

/// <summary>
///   Enumerates the available blend modes that determine the influence of components in a blend operation.
/// </summary>
public enum BlendMode
{
    /// <summary>
    ///   Specifies each component is multiplied by zero.
    /// </summary>
    Zero,

    /// <summary>
    ///   Specifies each component is multiplied by one.
    /// </summary>
    One,

    /// <summary>
    ///   Specifies each component is multiplied by the matching component of the source color.
    /// </summary>
    SourceColor,

    /// <summary>
    ///   Specifies each component is multiplied by (1 - the matching component of the source color).
    /// </summary>
    OneMinusSourceColor,

    /// <summary>
    ///   Specifies each component is multiplied by the matching component of the destination color.
    /// </summary>
    DestinationColor,

    /// <summary>
    ///   Specifies each component is multiplied by (1 - the matching component of the destination color).
    /// </summary>
    OneMinusDestinationColor,

    /// <summary>
    ///   Specifies each component is multiplied by the source alpha component.
    /// </summary>
    SourceAlpha,

    /// <summary>
    ///   Specifies each component is multiplied by (1 - source alpha).
    /// </summary>
    OneMinusSourceAlpha,

    /// <summary>
    ///   Specifies each component is multiplied by the destination alpha component.
    /// </summary>
    DestinationAlpha,

    /// <summary>
    ///   Specifies the component is multiplied by (1 - destination alpha).
    /// </summary>
    OneMinusDestinationAlpha,
}

/// <summary>
///   Represents the blend state used in a call to <see cref="IOutputMerger.SetBlendState(BlendStateDescription)"/>.
/// </summary>
/// <seealso cref="IEquatable{BlendStateDescription}"/>
public struct BlendStateDescription : IEquatable<BlendStateDescription>
{
    /// <summary>
    ///   The color used to calculate the <see cref="sourceMode"/> and <see cref="destinationMode"/> blending factors.
    /// </summary>
    private Color? color;

    /// <summary>
    ///   The blend mode that specifies how the red, green, blue, and alpha destination blending factors are computed.
    /// </summary>
    private BlendMode? destinationMode;

    /// <summary>
    ///   The equation mode that specifies how source and destination colors are combined.
    /// </summary>
    private BlendEquationMode? equationMode;

    /// <summary>
    ///   The blend mode that specifies how the red, green, blue, and alpha source blending factors are computed.
    /// </summary>
    private BlendMode? sourceMode;

    /// <summary>
    ///   Gets or sets a <see cref="System.Drawing.Color"/> that represents the color used to calculate the <see cref="SourceMode"/> and <see cref="DestinationMode"/> blending factors.
    /// </summary>
    /// <value>
    ///   The color used to calculate the <see cref="SourceMode"/> and <see cref="DestinationMode"/> blending factors.
    /// </value>
    public Color Color
    {
        get { return this.color ?? Color.Black; }
        set { this.color = value; }
    }

    /// <summary>
    ///   Gets or sets a <see cref="BlendMode"/> that represents the mode that specifies how the red, green, blue, and alpha destination blending factors are computed.
    /// </summary>
    /// <value>
    ///   The mode that specifies how the red, green, blue, and alpha destination blending factors are computed.
    /// </value>
    public BlendMode DestinationMode
    {
        get { return this.destinationMode ?? BlendMode.Zero; }
        set { this.destinationMode = value; }
    }

    /// <summary>
    ///   Gets or sets a value indicating whether blending is enabled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if blending is enabled; otherwise, <c>false</c>.
    /// </value>
    public bool Enabled { get; set; }

    /// <summary>
    ///   Gets or sets a <see cref="BlendEquationMode"/> that represents the mode that specifies how source and destination colors are combined.
    /// </summary>
    /// <value>
    ///   The mode that specifies how source and destination colors are combined.
    /// </value>
    public BlendEquationMode EquationMode
    {
        get { return this.equationMode ?? BlendEquationMode.Add; }
        set { this.equationMode = value; }
    }

    /// <summary>
    ///   Gets or sets a <see cref="BlendMode"/> that represents the mode that specifies how the red, green, blue, and alpha source blending factors are computed.
    /// </summary>
    /// <value>
    ///   The mode that specifies how the red, green, blue, and alpha source blending factors are computed.
    /// </value>
    public BlendMode SourceMode
    {
        get { return this.sourceMode ?? BlendMode.One; }
        set { this.sourceMode = value; }
    }

    /// <summary>
    ///   Implements the operator !=.
    /// </summary>
    /// <param name="left">
    ///   Specifies a <see cref="BlendStateDescription"/> that represents the left operand.
    /// </param>
    /// <param name="right">
    ///   Specifies a <see cref="BlendStateDescription"/> that represents the right operand.
    /// </param>
    /// <returns>
    ///   The result of the operator.
    /// </returns>
    public static bool operator !=(BlendStateDescription left, BlendStateDescription right)
    {
        return !(left == right);
    }

    /// <summary>
    ///   Implements the operator ==.
    /// </summary>
    /// <param name="left">
    ///   Specifies a <see cref="BlendStateDescription"/> that represents the left operand.
    /// </param>
    /// <param name="right">
    ///   Specifies a <see cref="BlendStateDescription"/> that represents the right operand.
    /// </param>
    /// <returns>
    ///   The result of the operator.
    /// </returns>
    public static bool operator ==(BlendStateDescription left, BlendStateDescription right)
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
    public bool Equals(BlendStateDescription other)
    {
        return this.Color == other.Color &&
               this.DestinationMode == other.DestinationMode &&
               this.Enabled == other.Enabled &&
               this.EquationMode == other.EquationMode &&
               this.SourceMode == other.SourceMode;
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
        return obj is BlendStateDescription description && this.Equals(description);
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

        return (this.Color.GetHashCode() * accumulator) +
               (this.DestinationMode.GetHashCode() * accumulator) +
               (this.Enabled.GetHashCode() * accumulator) +
               (this.EquationMode.GetHashCode() * accumulator) +
               (this.SourceMode.GetHashCode() * accumulator);
    }
}
