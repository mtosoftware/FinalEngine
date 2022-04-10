// <copyright file="ObservableCollectionFileNodeViewModelExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using FinalEngine.Editor.ViewModels.Docking.Tools;

    /// <summary>
    ///   Provides extensions for the <see cref="ObservableCollection{FileItemViewModel}"/> class.
    /// </summary>
    public static class ObservableCollectionFileNodeViewModelExtensions
    {
        /// <summary>
        ///   Constructs a hierarchy of file nodes by searching through the top level directories of all <paramref name="fileItems"/> at the specified <paramref name="location"/>.
        /// </summary>
        /// <param name="fileItems">
        ///   The file items.
        /// </param>
        /// <param name="location">
        ///   The location of the file nodes to attach to the specified <paramref name="fileItems"/>.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="fileItems"/> or <paramref name="location"/> parameter cannot be null.
        /// </exception>
        public static void ConstructHierarchy(this ObservableCollection<FileItemViewModel> fileItems, string location)
        {
            if (fileItems == null)
            {
                throw new ArgumentNullException(nameof(fileItems));
            }

            if (string.IsNullOrWhiteSpace(location))
            {
                throw new ArgumentNullException(nameof(location));
            }

            fileItems.Clear();

            IEnumerable<string> directories = Directory.EnumerateDirectories(location, string.Empty, SearchOption.TopDirectoryOnly);

            foreach (string directory in directories)
            {
                if (!File.GetAttributes(directory).CanIncludeInSearch())
                {
                    continue;
                }

                var directoryItem = new FileItemViewModel(FileItemType.Directory)
                {
                    Name = Path.GetFileName(directory),
                    Path = directory,
                };

                directoryItem.Children.Add(null!);

                fileItems.Add(directoryItem);
            }

            IEnumerable<string> files = Directory.EnumerateFiles(location, string.Empty, SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                if (!File.GetAttributes(file).CanIncludeInSearch())
                {
                    continue;
                }

                var fileItem = new FileItemViewModel(FileItemType.File)
                {
                    Name = Path.GetFileName(file),
                    Path = file,
                };

                fileItems.Add(fileItem);
            }
        }
    }
}