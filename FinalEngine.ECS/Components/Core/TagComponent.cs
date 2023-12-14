// <copyright file="TagComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Core;

using System.ComponentModel;

[Category("Core")]
public sealed class TagComponent : IEntityComponent, INotifyPropertyChanged
{
    private string? tag;

    public event PropertyChangedEventHandler? PropertyChanged;

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
