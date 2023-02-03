// <copyright file="IEnumMapper.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Utilities;

using System;

/// <summary>
/// Defines an interface that provides functions to swap between enumeration values.
/// </summary>
public interface IEnumMapper
{
    /// <summary>
    /// Gets the forward variant of the specified <paramref name="enumeration"/>.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the result.
    /// </typeparam>
    /// <param name="enumeration">
    /// The enumeration.
    /// </param>
    /// <returns>
    /// The forward variant of the specified <paramref name="enumeration"/>.
    /// </returns>
    TResult Forward<TResult>(Enum enumeration)
        where TResult : Enum;

    /// <summary>
    /// Gets the reverse variant of the specified <paramref name="enumeration"/>.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the result.
    /// </typeparam>
    /// <param name="enumeration">
    /// The enumeration.
    /// </param>
    /// <returns>
    /// The reverse variant of the specified <paramref name="enumeration"/>.
    /// </returns>
    TResult Reverse<TResult>(Enum enumeration)
        where TResult : Enum;
}
