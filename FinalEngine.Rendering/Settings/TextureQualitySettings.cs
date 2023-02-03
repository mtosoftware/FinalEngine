// <copyright file="TextureQualitySettings.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Settings;

using System;
using FinalEngine.Rendering.Textures;

public enum TextureFilterType
{
    NearestNeighbour,

    Bilinear,

    Trilinear,
}

public struct TextureQualitySettings : IEquatable<TextureQualitySettings>
{
    /// <summary>
    ///   The filter type.
    /// </summary>
    private TextureFilterType? filterType;

    /// <summary>
    ///   Gets or sets the filter type.
    /// </summary>
    /// <value>
    ///   The filter type.
    /// </value>
    public TextureFilterType FilterType
    {
        get { return this.filterType ?? TextureFilterType.Trilinear; }
        set { this.filterType = value; }
    }

    public TextureFilterMode MagFilter
    {
        get
        {
            return this.FilterType switch
            {
                TextureFilterType.NearestNeighbour => TextureFilterMode.Nearest,
                TextureFilterType.Bilinear => TextureFilterMode.Linear,
                TextureFilterType.Trilinear => TextureFilterMode.Linear,
                _ => TextureFilterMode.Linear,
            };
        }
    }

    public TextureFilterMode MinFilter
    {
        get
        {
            return this.FilterType switch
            {
                TextureFilterType.Bilinear => TextureFilterMode.Linear,
                TextureFilterType.Trilinear => TextureFilterMode.LinearMipmapLinear,
                _ => TextureFilterMode.LinearMipmapLinear,
            };
        }
    }

    /// <summary>
    ///   Implements the operator !=.
    /// </summary>
    /// <param name="left">
    ///   Specifies a <see cref="TextureQualitySettings"/> that represents the left operand.
    /// </param>
    /// <param name="right">
    ///   Specifies a <see cref="TextureQualitySettings"/> that represents the right operand.
    /// </param>
    /// <returns>
    ///   The result of the operator.
    /// </returns>
    public static bool operator !=(TextureQualitySettings left, TextureQualitySettings right)
    {
        return !(left == right);
    }

    /// <summary>
    ///   Implements the operator ==.
    /// </summary>
    /// <param name="left">
    ///   Specifies a <see cref="TextureQualitySettings"/> that represents the left operand.
    /// </param>
    /// <param name="right">
    ///   Specifies a <see cref="TextureQualitySettings"/> that represents the right operand.
    /// </param>
    /// <returns>
    ///   The result of the operator.
    /// </returns>
    public static bool operator ==(TextureQualitySettings left, TextureQualitySettings right)
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
    public bool Equals(TextureQualitySettings other)
    {
        return this.FilterType == other.FilterType;
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
        return obj is TextureQualitySettings settings && this.Equals(settings);
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

        return this.FilterType.GetHashCode() * accumulator;
    }
}
