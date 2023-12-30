// <copyright file="MockDisposableResource.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Resources.Mocks;

using System;
using FinalEngine.Resources;

public sealed class MockDisposableResource : IResource, IDisposable
{
    public bool IsDisposed { get; private set; }

    public void Dispose()
    {
        this.IsDisposed = true;
    }
}
