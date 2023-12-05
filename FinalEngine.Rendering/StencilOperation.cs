// <copyright file="StencilOperation.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

public enum StencilOperation
{
    Keep,

    Zero,

    Replace,

    Increment,

    IncrementWrap,

    Decrement,

    DecrementWrap,

    Invert,
}
