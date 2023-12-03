// <copyright file="IEntityComponentCategoryViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System.Collections.Generic;

public interface IEntityComponentCategoryViewModel
{
    IEnumerable<IEntityComponentTypeViewModel> ComponentTypes { get; }

    string Name { get; }
}
