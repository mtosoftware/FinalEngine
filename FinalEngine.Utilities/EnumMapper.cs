// <copyright file="EnumMapper.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Utilities
{
    using System;
    using System.Collections.Generic;

    public class EnumMapper : IEnumMapper
    {
        private readonly IReadOnlyDictionary<Enum, Enum> forwardToReverseMap;

        private readonly IReadOnlyDictionary<Enum, Enum> reverseToForwardMap;

        public EnumMapper(IReadOnlyDictionary<Enum, Enum> forwardToReverseMap)
        {
            this.forwardToReverseMap = forwardToReverseMap ?? throw new ArgumentNullException(nameof(forwardToReverseMap), $"The specified {nameof(forwardToReverseMap)} parameter cannot be null.");
            this.reverseToForwardMap = new Dictionary<Enum, Enum>();

            foreach (var key in forwardToReverseMap.Keys)
            {
                ((IDictionary<Enum, Enum>)this.reverseToForwardMap).Add(forwardToReverseMap[key], key);
            }
        }

        public TResult Forward<TResult>(Enum enumeration)
            where TResult : Enum
        {
            return Get<TResult>(this.forwardToReverseMap, enumeration);
        }

        public TResult Reverse<TResult>(Enum enumeration)
            where TResult : Enum
        {
            return Get<TResult>(this.reverseToForwardMap, enumeration);
        }

        private static TResult Get<TResult>(IReadOnlyDictionary<Enum, Enum> map, Enum enumeration)
            where TResult : Enum
        {
            if (enumeration == null)
            {
                throw new ArgumentNullException(nameof(enumeration), $"The specified {nameof(enumeration)} parameter cannot be null.");
            }

            try
            {
                return (TResult)map[enumeration];
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException($"The specified {nameof(enumeration)} couldn't be located by the enumeration mapper.");
            }
        }
    }
}
