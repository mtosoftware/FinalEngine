// <copyright file="IEnumMapper.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Utilities
{
    using System;

    public interface IEnumMapper
    {
        TResult Forward<TResult>(Enum enumeration)
            where TResult : Enum;

        TResult Reverse<TResult>(Enum enumeration)
            where TResult : Enum;
    }
}
