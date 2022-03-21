// <copyright file="IDockViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking
{
    using System.Collections.Generic;

    public interface IDockViewModel
    {
        IEnumerable<IPaneViewModel> Documents { get; }

        IEnumerable<IToolViewModel> Tools { get; }
    }
}