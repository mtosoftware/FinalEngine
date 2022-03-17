// <copyright file="DirectoryNodeExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using FinalEngine.Editor.Common.Models;

    public static class ObservableCollectionFileNodeExtensions
    {
        public static void ConstructHierarchy(this ObservableCollection<FileNode> fileNodes, string location)
        {
            if (fileNodes == null)
            {
                throw new ArgumentNullException(nameof(fileNodes));
            }

            if (string.IsNullOrWhiteSpace(location))
            {
                throw new ArgumentNullException(nameof(location));
            }

            fileNodes.Clear();

            IEnumerable<string> directories = Directory.EnumerateDirectories(location, string.Empty, SearchOption.TopDirectoryOnly);

            foreach (string directory in directories)
            {
                var node = new DirectoryNode()
                {
                    Name = Path.GetFileName(directory),
                    DirectoryPath = directory,
                };

                fileNodes.Add(node);
            }

            IEnumerable<string> files = Directory.EnumerateFiles(location, string.Empty, SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                var node = new FileNode()
                {
                    Name = Path.GetFileName(file),
                    DirectoryPath = file,
                };

                fileNodes.Add(node);
            }
        }
    }
}