// <copyright file="FileNameAttribute.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Validation;

using System;
using System.ComponentModel.DataAnnotations;
using System.IO.Abstractions;

/// <summary>
/// Provides a <see cref="ValidationAttribute"/> that determines whether a <see cref="string"/> is a valid file name.
/// </summary>
/// <seealso cref="ValidationAttribute" />
public sealed class FileNameAttribute : ValidationAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileNameAttribute"/> class.
    /// </summary>
    public FileNameAttribute()
        : this(new FileSystem())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileNameAttribute"/> class.
    /// </summary>
    /// <param name="fileSystem">
    /// The file system, used to detrmine whether the value is a valid file name.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="fileSystem"/> parameter cannot be null.
    /// </exception>
    public FileNameAttribute(IFileSystem fileSystem)
    {
        this.FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    /// <summary>
    /// Gets the file system.
    /// </summary>
    /// <value>
    /// The file system, used to determine whether the value is a valid file name.
    /// </value>
    public IFileSystem FileSystem { get; }

    /// <summary>
    /// Returns true if the specified <paramref name="value"/> is a valid file name.
    /// </summary>
    /// <param name="value">
    /// The value to validate.
    /// </param>
    /// <param name="validationContext">
    /// The context information about the validation operation.
    /// </param>
    /// <returns>
    /// An instance of a <see cref="ValidationResult"/> that can be used to determine whether the <paramref name="value"/> is a valid file name.
    /// </returns>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult(this.ErrorMessage);
        }

        string? fileName = value.ToString();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            return new ValidationResult(this.ErrorMessage);
        }

        if (fileName.IndexOfAny(this.FileSystem.Path.GetInvalidFileNameChars()) < 0)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(this.ErrorMessage);
    }
}
