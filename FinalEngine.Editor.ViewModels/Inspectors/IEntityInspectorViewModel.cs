// <copyright file="IEntityInspectorViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System.Collections.Generic;

public interface IEntityInspectorViewModel
{
    ICollection<IEntityComponentCategoryViewModel> CategorizedComponentTypes { get; }

    ICollection<IEntityComponentViewModel> ComponentViewModels { get; }
}
