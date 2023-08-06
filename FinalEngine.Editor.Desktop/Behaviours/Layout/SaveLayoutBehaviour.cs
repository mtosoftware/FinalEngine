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

    private static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
        "Command",
        typeof(ICommand),
        typeof(SaveLayoutBehaviour),
        new PropertyMetadata(null, OnCommandChanged));

    public static ICommand GetCommand(DependencyObject dependencyObject)
    {
        if (dependencyObject == null)
        {
            throw new ArgumentNullException(nameof(dependencyObject));
        }

        return (ICommand)dependencyObject.GetValue(CommandProperty);
    }

    public static object GetCommandParameter(DependencyObject dependencyObject)
    {
        if (dependencyObject == null)
        {
            throw new ArgumentNullException(nameof(dependencyObject));
        }

        return dependencyObject.GetValue(CommandParameterProperty);
    }

    public static void SetCommand(DependencyObject dependencyObject, ICommand value)
    {
        if (dependencyObject == null)
        {
            throw new ArgumentNullException(nameof(dependencyObject));
        }

        dependencyObject.SetValue(CommandProperty, value);
    }

    public static void SetCommandParameter(DependencyObject dependencyObject, object value)
    {
        if (dependencyObject == null)
        {
            throw new ArgumentNullException(nameof(dependencyObject));
        }

        dependencyObject.SetValue(CommandParameterProperty, value);
    }

    private static void OnCommandChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
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

    private static void OnFrameworkElement_Saveed(object sender, RoutedEventArgs e)
    {
        if (sender is not DockingManager frameworkElement)
        {
            return;
        }

        var command = GetCommand(frameworkElement);

        if (command == null)
        {
            return;
        }

        command.Execute(GetCommandParameter(frameworkElement));
    }
}
