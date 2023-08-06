// <copyright file="LoadLayoutBehaviour.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Behaviours.Layout;

using System;
using System.Windows;
using System.Windows.Input;

public static class LoadLayoutBehaviour
{
    private static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached(
        "CommandParameter",
        typeof(object),
        typeof(LoadLayoutBehaviour),
        new PropertyMetadata(null));

    private static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
        "Command",
        typeof(ICommand),
        typeof(LoadLayoutBehaviour),
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
            throw new ArgumentException($"The specified {nameof(DependencyObject)} is not of type {nameof(FrameworkElement)}.");
        }

        framworkElement.Loaded -= OnFrameworkElement_Loaded;

        if (e.NewValue is ICommand command)
        {
            framworkElement.Loaded += OnFrameworkElement_Loaded;
        }
    }

    private static void OnFrameworkElement_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement frameworkElement)
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
