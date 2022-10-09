// <copyright file="SelectedIndexChangedEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Views.Events
{
    public class SelectedIndexChangedEventArgs
    {
        public SelectedIndexChangedEventArgs(int index)
        {
            this.Index = index;
        }

        public int Index { get; set; }
    }
}
