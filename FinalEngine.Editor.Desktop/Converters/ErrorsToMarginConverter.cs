// <copyright file="ErrorsToMarginConverter.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Converters;

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

/// <summary>
///   Provides a converter from a list of errors to the vertical margin of all errors.
/// </summary>
/// <seealso cref="System.Windows.Data.IValueConverter"/>
public class ErrorsToMarginConverter : IValueConverter
{
    /// <summary>
    ///   Converts a value.
    /// </summary>
    /// <param name="value">
    ///   The value produced by the binding source.
    /// </param>
    /// <param name="targetType">
    ///   The type of the binding target property.
    /// </param>
    /// <param name="parameter">
    ///   The converter parameter to use.
    /// </param>
    /// <param name="culture">
    ///   The culture to use in the converter.
    /// </param>
    /// <returns>
    ///   A converted value. If the method returns <c>null</c>, the valid null value is used.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///   The specified {nameof(value)} parameter is not of type {typeof(int)}.
    /// </exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not int)
        {
            throw new ArgumentException($"The specified {nameof(value)} parameter is not of type {typeof(int)}.");
        }

        int errors = (int)value;
        return new Thickness(10, 10, 10, errors * 20);
    }

    /// <summary>
    ///   Converts a value.
    /// </summary>
    /// <param name="value">
    ///   The value that is produced by the binding target.
    /// </param>
    /// <param name="targetType">
    ///   The type to convert to.
    /// </param>
    /// <param name="parameter">
    ///   The converter parameter to use.
    /// </param>
    /// <param name="culture">
    ///   The culture to use in the converter.
    /// </param>
    /// <returns>
    ///   A converted value. If the method returns <c>null</c>, the valid null value is used.
    /// </returns>
    public object? ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
