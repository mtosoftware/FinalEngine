// <copyright file="DirectoryAttribute.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using FinalEngine.IO;

    public class DirectoryAttribute : ValidationAttribute
    {
        public DirectoryAttribute()
            : this(new FileSystem())
        {
        }

        public DirectoryAttribute(IFileSystem fileSystem)
        {
            this.FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public IFileSystem FileSystem { get; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("You must specify a directory location.");
            }

            string? directory = value.ToString();

            if (directory == null)
            {
                return new ValidationResult("You must specify a directory location.");
            }

            if (!this.FileSystem.IsValidDirectory(directory))
            {
                return new ValidationResult("You must specify a valid directory location.");
            }

            return ValidationResult.Success;
        }
    }
}