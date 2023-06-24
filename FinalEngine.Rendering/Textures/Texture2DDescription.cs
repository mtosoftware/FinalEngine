// <copyright file="Texture2DDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

using System;

/// <summary>
///   Represents the description of a 2D texture used in a call to <see cref="IGPUResourceFactory.CreateTexture2D{T}(Texture2DDescription, System.Collections.Generic.IReadOnlyCollection{T}?, PixelFormat, SizedFormat)"/>.
/// </summary>
/// <seealso cref="IEquatable{Texture2DDescription}"/>
public struct Texture2DDescription : IEquatable<Texture2DDescription>
{
    /// <summary>
    ///   The texture magnification filter used when sampling from the texture determines that the texture should be magnified.
    /// </summary>
    private TextureFilterMode? magFilter;

    /// <summary>
    ///   The texture minifying filter used when sampling from the texture determines that the texture should be minified.
    /// </summary>
    private TextureFilterMode? minFilter;

    /// <summary>
    ///   The data type of the pixel data.
    /// </summary>
    private PixelType? pixelType;

    /// <summary>
    ///   The texture wrap mode for the S/X direction.
    /// </summary>
    private TextureWrapMode? wrapS;

    /// <summary>
    ///   The texture wrap mode for the T/Y direction.
    /// </summary>
    private TextureWrapMode? wrapT;

    /// <summary>
    /// Gets or sets a value indicating whether the texture should generate mipmaps.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the texture should generate mipmaps; otherwise, <c>false</c>.
    /// </value>
    public bool GenerateMipmaps { get; set; }

    /// <summary>
    ///   Gets or sets the height of the texture.
    /// </summary>
    /// <value>
    ///   The height of the texture.
    /// </value>
    public int Height { get; set; }

    /// <summary>
    ///   Gets or sets the texture magnification filter used when sampling from the texture determines that the texture should be magnified.
    /// </summary>
    /// <value>
    ///   The texture magnification filter used when sampling from the texture determines that the texture should be magnified.
    /// </value>
    public TextureFilterMode MagFilter
    {
        get { return this.magFilter ?? TextureFilterMode.Linear; }
        set { this.magFilter = value; }
    }

    /// <summary>
    ///   Gets or sets the texture minifying filter used when sampling from the texture determines that the texture should be minified.
    /// </summary>
    /// <value>
    ///   The texture minifying filter used when sampling from the texture determines that the texture should be minified.
    /// </value>
    public TextureFilterMode MinFilter
    {
        get { return this.minFilter ?? TextureFilterMode.Linear; }
        set { this.minFilter = value; }
    }

    /// <summary>
    ///   Gets or sets the data type of the pixel data.
    /// </summary>
    /// <value>
    ///   The data type of the pixel data.
    /// </value>
    public PixelType PixelType
    {
        get { return this.pixelType ?? PixelType.Byte; }
        set { this.pixelType = value; }
    }

    /// <summary>
    ///   Gets or sets the width of the texture.
    /// </summary>
    /// <value>
    ///   The width of the texture.
    /// </value>
    public int Width { get; set; }

    /// <summary>
    ///   Gets or sets the texture wrap mode for the S/X direction.
    /// </summary>
    /// <value>
    ///   The texture wrap mode for the S/X direction.
    /// </value>
    public TextureWrapMode WrapS
    {
        get { return this.wrapS ?? TextureWrapMode.Repeat; }
        set { this.wrapS = value; }
    }

    /// <summary>
    ///   Gets or sets the texture wrap mode for the T/Y direction.
    /// </summary>
    /// <value>
    ///   The texture wrap mode for the T/Y direction.
    /// </value>
    public TextureWrapMode WrapT
    {
        get { return this.wrapT ?? TextureWrapMode.Repeat; }
        set { this.wrapT = value; }
    }

    /// <summary>
    ///   Implements the operator !=.
    /// </summary>
    /// <param name="left">
    ///   Specifies a <see cref="Texture2DDescription"/> that represents the left operand.
    /// </param>
    /// <param name="right">
    ///   Specifies a <see cref="Texture2DDescription"/> that represents the right operand.
    /// </param>
    /// <returns>
    ///   The result of the operator.
    /// </returns>
    public static bool operator !=(Texture2DDescription left, Texture2DDescription right)
    {
        return !(left == right);
    }

    /// <summary>
    ///   Implements the operator ==.
    /// </summary>
    /// <param name="left">
    ///   Specifies a <see cref="Texture2DDescription"/> that represents the left operand.
    /// </param>
    /// <param name="right">
    ///   Specifies a <see cref="Texture2DDescription"/> that represents the right operand.
    /// </param>
    /// <returns>
    ///   The result of the operator.
    /// </returns>
    public static bool operator ==(Texture2DDescription left, Texture2DDescription right)
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
    public bool Equals(Texture2DDescription other)
    {
        return this.MinFilter == other.MinFilter &&
               this.MagFilter == other.magFilter &&
               this.WrapS == other.WrapS &&
               this.WrapT == other.WrapT &&
               this.PixelType == other.PixelType &&
               this.Width == other.Width &&
               this.Height == other.Height;
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
        return obj is Texture2DDescription description && this.Equals(description);
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

        return (this.MinFilter.GetHashCode() * accumulator) +
               (this.MagFilter.GetHashCode() * accumulator) +
               (this.WrapS.GetHashCode() * accumulator) +
               (this.WrapT.GetHashCode() * accumulator) +
               (this.PixelType.GetHashCode() * accumulator) +
               (this.Width.GetHashCode() * accumulator) +
               (this.Height.GetHashCode() * accumulator);
    }
}
