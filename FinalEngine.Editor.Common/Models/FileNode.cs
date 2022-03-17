// <copyright file="FileNode.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models
{
    public class FileNode
    {
        public string DirectoryPath { get; init; }

        public string Name { get; init; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}