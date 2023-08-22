// <copyright file="TagComponent.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Core;

using System.ComponentModel;

/// <summary>
/// Provides a component that represents a name or tag for an <see cref="Entity"/>.
/// </summary>
/// <seealso cref="IEntityComponent" />
public sealed class TagComponent : IEntityComponent, INotifyPropertyChanged
{
    /// <summary>
    /// The tag.
    /// </summary>
    private string? tag;

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets or sets the tag.
    /// </summary>
    /// <value>
    /// The tag.
    /// </value>
    public string? Tag
    {
        get
        {
            return this.tag;
        }

        set
        {
            if (this.tag == value)
            {
                return;
            }

            this.tag = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Tag)));
        }
    }
}
