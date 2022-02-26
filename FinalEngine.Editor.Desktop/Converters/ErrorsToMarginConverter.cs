// <copyright file="ErrorToMarginConverter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class ErrorsToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not int)
            {
                throw new ArgumentException($"The specified {nameof(value)} parameter is not of type {typeof(int)}.");
            }

            int errors = (int)value;
            return new Thickness(10, 10, 10, errors * 20);
        }

        public object? ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}