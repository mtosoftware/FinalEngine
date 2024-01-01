// <copyright file="IFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Factories;

public interface IFactory<out T>
{
    T Create();
}
