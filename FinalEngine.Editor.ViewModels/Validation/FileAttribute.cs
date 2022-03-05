// <copyright file="FileAttribute.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using FinalEngine.IO;

    /// <summary>
    ///   Specifies that a data field is a valid file name.
    /// </summary>
    /// <seealso cref="ValidationAttribute"/>
    public sealed class FileAttribute : ValidationAttribute
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="FileAttribute"/> class.
        /// </summary>
        public FileAttribute()
            : this(new FileSystem())
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="FileAttribute"/> class.
        /// </summary>
        /// <param name="fileSystem">
        ///   The file system.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="fileSystem"/> parameter cannot be null.
        /// </exception>
        public FileAttribute(IFileSystem fileSystem)
        {
            this.FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        /// <summary>
        ///   Gets the file system.
        /// </summary>
        /// <value>
        ///   The file system.
        /// </value>
        public IFileSystem FileSystem { get; }

        /// <summary>
        ///   Returns true if the specified <paramref name="value"/> is a valid file name..
        /// </summary>
        /// <param name="value">
        ///   The value to validate.
        /// </param>
        /// <param name="validationContext">
        ///   The context information about the validation operation.
        /// </param>
        /// <returns>
        ///   An instance of the <see cref="ValidationResult"/> class.
        /// </returns>
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