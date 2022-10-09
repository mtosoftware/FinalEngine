// <copyright file="SceneHierarchyToolWindowViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Tools
{
    using System;
    using System.ComponentModel;
    using FinalEngine.ECS;
    using FinalEngine.Editor.Views.Tools;

    public class SceneHierarchyToolWindowViewModel : ViewModelBase
    {
        private readonly ISceneHierarchyToolWindowView view;

        private bool canPerformEditCommands;

        private BindingList<Entity>? entities;

        private int selectedIndex;

        public SceneHierarchyToolWindowViewModel(ISceneHierarchyToolWindowView view)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));

            this.view.OnContextMenuOpening += this.View_OnContextMenuOpening;
            this.view.OnClick += this.View_OnMouseDown;

            this.Entities.Add(new Entity());
            this.Entities.Add(new Entity());
        }

        public bool CanPerformEditCommands
        {
            get { return this.canPerformEditCommands; }
            private set { this.SetProperty(ref this.canPerformEditCommands, value); }
        }

        public BindingList<Entity> Entities
        {
            get { return this.entities ??= new BindingList<Entity>(); }
        }

        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }

            set
            {
                this.SetProperty(ref this.selectedIndex, value);
            }
        }

        private Entity? SelectedEntity
        {
            get
            {
                if (this.SelectedIndex == -1)
                {
                    return null;
                }

                return this.Entities[this.SelectedIndex];
            }
        }

        private void View_OnContextMenuOpening(object? sender, CancelEventArgs e)
        {
            this.CanPerformEditCommands = this.SelectedEntity != null;
        }

        private void View_OnMouseDown(object? sender, EventArgs e)
        {
            this.SelectedIndex = -1;
        }
    }
}
