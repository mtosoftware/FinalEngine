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

    private static readonly DependencyProperty LoadLayoutCommandProperty = DependencyProperty.RegisterAttached(
        "LoadLayoutCommand",
        typeof(ICommand),
        typeof(LoadLayoutBehaviour),
        new PropertyMetadata(null, OnLoadLayoutCommandChanged));

    public static object GetCommandParameter(DependencyObject dependencyObject)
    {
        if (dependencyObject == null)
        {
            throw new ArgumentNullException(nameof(dependencyObject));
        }

        return dependencyObject.GetValue(CommandParameterProperty);
    }

    public static ICommand GetLoadLayoutCommand(DependencyObject dependencyObject)
    {
        if (dependencyObject == null)
        {
            throw new ArgumentNullException(nameof(dependencyObject));
        }

        return (ICommand)dependencyObject.GetValue(LoadLayoutCommandProperty);
    }

    public static void SetCommandParameter(DependencyObject dependencyObject, object value)
    {
        if (dependencyObject == null)
        {
            throw new ArgumentNullException(nameof(dependencyObject));
        }

        dependencyObject.SetValue(CommandParameterProperty, value);
    }

    public static void SetLoadLayoutCommand(DependencyObject dependencyObject, ICommand value)
    {
        if (dependencyObject == null)
        {
            throw new ArgumentNullException(nameof(dependencyObject));
        }

        dependencyObject.SetValue(LoadLayoutCommandProperty, value);
    }

    private static void OnFrameworkElement_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement frameworkElement)
        {
            return;
        }

        var loadLayoutCommand = GetLoadLayoutCommand(frameworkElement);

        if (loadLayoutCommand == null)
        {
            return;
        }

        loadLayoutCommand.Execute(GetCommandParameter(frameworkElement));
    }

    private static void OnLoadLayoutCommandChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
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
}
