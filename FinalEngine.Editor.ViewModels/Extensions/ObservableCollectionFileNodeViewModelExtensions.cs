namespace FinalEngine.Editor.ViewModels.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using FinalEngine.Editor.ViewModels.Docking.Tools;

    public static class ObservableCollectionFileNodeViewModelExtensions
    {
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

                var directoryItem = new FileItemViewModel()
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

                var fileItem = new FileItemViewModel()
                {
                    Name = Path.GetFileName(file),
                    Path = file,
                };

                fileItems.Add(fileItem);
            }
        }
    }
}