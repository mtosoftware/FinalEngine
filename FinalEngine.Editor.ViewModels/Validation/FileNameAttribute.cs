// <copyright file="FileNameAttribute.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Validation;

using System;
using System.ComponentModel.DataAnnotations;
using System.IO.Abstractions;

public sealed class FileNameAttribute : ValidationAttribute
{
    public FileNameAttribute()
        : this(new FileSystem())
    {
    }

    public FileNameAttribute(IFileSystem fileSystem)
    {
        this.FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public IFileSystem FileSystem { get; }

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
