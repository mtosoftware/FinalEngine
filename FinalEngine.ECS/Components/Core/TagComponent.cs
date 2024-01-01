// <copyright file="TagComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Core;

using System.ComponentModel;

[Category("Core")]
public sealed class TagComponent : IEntityComponent, INotifyPropertyChanged
{
    private string? name;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string? Name
    {
        get
        {
            return this.name;
        }

        set
        {
            if (this.name == value)
            {
                return;
            }

            this.name = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Name)));
        }
    }
}
