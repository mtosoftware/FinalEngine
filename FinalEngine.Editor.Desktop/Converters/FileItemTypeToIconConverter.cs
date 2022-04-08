// <copyright file="FileItemTypeToIconConverter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using FinalEngine.Editor.ViewModels.Docking.Tools;

    public class FileItemTypeToIconConverter : IValueConverter
    {
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}