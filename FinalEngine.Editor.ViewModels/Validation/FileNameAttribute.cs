// <copyright file="FileNameAttribute.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Validation;

using System.ComponentModel.DataAnnotations;
using System.IO;

public sealed class FileNameAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return null;
        }

        string? fileName = value.ToString();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            return new ValidationResult(this.ErrorMessage);
        }

        if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(this.ErrorMessage);
    }
}
