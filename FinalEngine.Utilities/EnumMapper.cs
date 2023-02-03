// <copyright file="EnumMapper.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Utilities;

using System;
using System.Collections.Generic;

/// <summary>
/// Provides a concrete implementation of an <see cref="IEnumMapper"/>.
/// </summary>
/// <seealso cref="FinalEngine.Utilities.IEnumMapper" />
public class EnumMapper : IEnumMapper
{
    /// <summary>
    /// The forward to reverse map.
    /// </summary>
    private readonly IReadOnlyDictionary<Enum, Enum> forwardToReverseMap;

    /// <summary>
    /// The reverse to forward map.
    /// </summary>
    private readonly IReadOnlyDictionary<Enum, Enum> reverseToForwardMap;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumMapper"/> class.
    /// </summary>
    /// <param name="forwardToReverseMap">
    /// The forward to reverse map.
    /// </param>
    /// <exception cref="System.ArgumentNullException">
    /// The specified <paramref name="forwardToReverseMap"/> parameter cannot be null.
    /// </exception>
    public EnumMapper(IReadOnlyDictionary<Enum, Enum> forwardToReverseMap)
    {
        this.forwardToReverseMap = forwardToReverseMap ?? throw new ArgumentNullException(nameof(forwardToReverseMap));
        this.reverseToForwardMap = new Dictionary<Enum, Enum>();

        foreach (var key in forwardToReverseMap.Keys)
        {
            ((IDictionary<Enum, Enum>)this.reverseToForwardMap).Add(forwardToReverseMap[key], key);
        }
    }

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
    public TResult Forward<TResult>(Enum enumeration)
        where TResult : Enum
    {
        return Get<TResult>(this.forwardToReverseMap, enumeration);
    }

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
    public TResult Reverse<TResult>(Enum enumeration)
        where TResult : Enum
    {
        return Get<TResult>(this.reverseToForwardMap, enumeration);
    }

    /// <summary>
    /// Gets the variant from the specified <paramref name="map"/> using the specified <paramref name="enumeration"/>.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the result.
    /// </typeparam>
    /// <param name="map">
    /// The variant map.
    /// </param>
    /// <param name="enumeration">
    /// The enumeration.
    /// </param>
    /// <returns>
    /// The variant enumeration.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="enumeration"/> or <paramref name="map"/> parameter cannot be null.
    /// </exception>
    /// <exception cref="KeyNotFoundException">
    /// The specified <paramref name="enumeration"/> couldn't be located by the enumeration mapper.
    /// </exception>
    private static TResult Get<TResult>(IReadOnlyDictionary<Enum, Enum> map, Enum enumeration)
        where TResult : Enum
    {
        if (enumeration == null)
        {
            throw new ArgumentNullException(nameof(enumeration));
        }

        if (map == null)
        {
            throw new ArgumentNullException(nameof(map));
        }

        try
        {
            return (TResult)map[enumeration];
        }
        catch (KeyNotFoundException)
        {
            throw new KeyNotFoundException($"The specified {nameof(enumeration)} couldn't be located by the enumeration mapper.");
        }
    }
}
