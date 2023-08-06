// <copyright file="SaveLayoutBehaviour.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Behaviours.Layout;

using System;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Input;
using AvalonDock;

[SupportedOSPlatform("windows")]
public static class SaveLayoutBehaviour
{
    private static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached(
        "CommandParameter",
        typeof(object),
        typeof(SaveLayoutBehaviour),
        new PropertyMetadata(null));

    private static readonly DependencyProperty SaveLayoutCommandProperty = DependencyProperty.RegisterAttached(
        "SaveLayoutCommand",
        typeof(ICommand),
        typeof(SaveLayoutBehaviour),
        new PropertyMetadata(null, OnSaveLayoutCommandChanged));

    public static object GetCommandParameter(DependencyObject dependencyObject)
    {
        if (dependencyObject == null)
        {
            throw new ArgumentNullException(nameof(dependencyObject));
        }

        return dependencyObject.GetValue(CommandParameterProperty);
    }

    public static ICommand GetSaveLayoutCommand(DependencyObject dependencyObject)
    {
        if (dependencyObject == null)
        {
            throw new ArgumentNullException(nameof(dependencyObject));
        }

        return (ICommand)dependencyObject.GetValue(SaveLayoutCommandProperty);
    }

    public static void SetCommandParameter(DependencyObject dependencyObject, object value)
    {
        if (dependencyObject == null)
        {
            throw new ArgumentNullException(nameof(dependencyObject));
        }

        dependencyObject.SetValue(CommandParameterProperty, value);
    }

    public static void SetSaveLayoutCommand(DependencyObject dependencyObject, ICommand value)
    {
        if (dependencyObject == null)
        {
            throw new ArgumentNullException(nameof(dependencyObject));
        }

        dependencyObject.SetValue(SaveLayoutCommandProperty, value);
    }

    private static void OnFrameworkElement_Saveed(object sender, RoutedEventArgs e)
    {
        if (sender is not DockingManager frameworkElement)
        {
            return;
        }

        var saveLayoutCommand = GetSaveLayoutCommand(frameworkElement);

        if (saveLayoutCommand == null)
        {
            return;
        }

        saveLayoutCommand.Execute(GetCommandParameter(frameworkElement));
    }

    private static void OnSaveLayoutCommandChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        if (dependencyObject is not FrameworkElement framworkElement)
        {
            throw new ArgumentException($"The specified {nameof(dependencyObject)} parameter is not of type {nameof(FrameworkElement)}.");
        }

        framworkElement.Unloaded -= OnFrameworkElement_Saveed;

        if (e.NewValue is ICommand command)
        {
            framworkElement.Unloaded += OnFrameworkElement_Saveed;
        }
    }
}
