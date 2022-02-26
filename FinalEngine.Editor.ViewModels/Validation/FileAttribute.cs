// <copyright file="FileAttribute.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using FinalEngine.IO;

    public class FileAttribute : ValidationAttribute
    {
        public FileAttribute(IFileSystem fileSystem)
        {
            this.FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public FileAttribute()
            : this(new FileSystem())
        {
        }

        public IFileSystem FileSystem { get; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("You must specify a file name.");
            }

            string? fileName = value.ToString();

            if (fileName == null)
            {
                return new ValidationResult("You must specify a file name.");
            }

            if (!this.FileSystem.IsValidFileName(fileName))
            {
                return new ValidationResult("You must specify a valid file name.");
            }

            return ValidationResult.Success;
        }
    }
}