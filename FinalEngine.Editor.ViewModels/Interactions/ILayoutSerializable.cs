// <copyright file="ILayoutSerializable.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interactions;

public interface ILayoutSerializable
{
    void Deserialize(string content);

    string Serialize();
}
