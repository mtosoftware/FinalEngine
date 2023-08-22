// <copyright file="IsAnyToken.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Messages;

using System;
using Moq;

[TypeMatcher]
public sealed class IsAnyToken : ITypeMatcher, IEquatable<IsAnyToken>
{
    public bool Equals(IsAnyToken other)
    {
        if (other == null)
        {
            return false;
        }

        return ReferenceEquals(this, other);
    }

    public override bool Equals(object obj)
    {
        return this.Equals(obj as IsAnyToken);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public bool Matches(Type typeArgument)
    {
        return true;
    }
}
