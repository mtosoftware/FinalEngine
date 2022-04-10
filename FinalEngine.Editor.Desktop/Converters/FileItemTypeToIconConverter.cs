// <copyright file="FileItemTypeToIconConverter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using FinalEngine.Editor.ViewModels.Docking.Tools;

    /// <summary>
    ///   Provides a converter from a <see cref="FileItemType"/> to an icon source.
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter"/>
    public class FileItemTypeToIconConverter : IValueConverter
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
        ///   A converted value. If the method returns <see langword="null"/>, the valid null value is used.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        ///   The specified <paramref name="value"/> parameter is not of type <see cref="IFileItemViewModel"/>.
        /// </exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not FileItemViewModel fileItem)
            {
                throw new ArgumentException($"The specified {nameof(value)} parameter is not of type {nameof(FileItemType)}.", nameof(value));
            }

            switch (fileItem.Type)
            {
                case FileItemType.Directory:
                    return fileItem.IsExpanded ? Resources.FolderOpenedIconUri : Resources.FolderClosedIconUri;

                case FileItemType.File:
                    return Resources.FileIconUri;
            }

            return Resources.FileIconUri;
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
        ///   A converted value. If the method returns <see langword="null"/>, the valid null value is used.
        /// </returns>
        /// <exception cref="System.NotSupportedException">
        ///   Convert from an icon back to a <see cref="FileItemType"/> is not supported.
        /// </exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}