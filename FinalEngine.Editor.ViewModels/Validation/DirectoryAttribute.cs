// <copyright file="DirectoryAttribute.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.IO;

    /// <summary>
    ///   Specifies that a data field is a valid directory path.
    /// </summary>
    /// <seealso cref="ValidationAttribute"/>
    public sealed class DirectoryAttribute : ValidationAttribute
    {
        /// <summary>
        ///   The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        ///   Initializes a new instance of the <see cref="DirectoryAttribute"/> class.
        /// </summary>
        public DirectoryAttribute()
            : this(new FileSystem())
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="DirectoryAttribute"/> class.
        /// </summary>
        /// <param name="fileSystem">
        ///   The file system.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="fileSystem"/> parameter cannot be null.
        /// </exception>
        [SuppressMessage("Design", "CA1019:Define accessors for attribute arguments", Justification = "Unit Testing")]
        public DirectoryAttribute(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public bool ShouldExist { get; set; }

        /// <summary>
        ///   Returns true if the specified <paramref name="value"/> is a valid directory.
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
                return new ValidationResult("You must specify a directory location.");
            }

            string? directory = value.ToString();

            if (directory == null)
            {
                return new ValidationResult("You must specify a directory location.");
            }

            if (!this.fileSystem.IsValidDirectory(directory))
            {
                return new ValidationResult("You must specify a valid directory location.");
            }

            if (this.ShouldExist && !this.fileSystem.DirectoryExists(directory))
            {
                return new ValidationResult("The directory location doesn't exist.");
            }

            return ValidationResult.Success;
        }
    }
}