// <copyright file="FileNameAttribute.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Validation;

using System;
using System.ComponentModel.DataAnnotations;
using System.IO.Abstractions;

public sealed class FileNameAttribute : ValidationAttribute
{
    private IFileSystem fileSystem;

    public FileNameAttribute()
        : this(new FileSystem())
    {
    }

    public FileNameAttribute(IFileSystem fileSystem)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

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

        if (fileName.IndexOfAny(this.fileSystem.Path.GetInvalidFileNameChars()) < 0)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(this.ErrorMessage);
    }
}
