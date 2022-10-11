namespace FinalEngine.Editor.Services.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t =>
                {
                    return t != null;
                });
            }
        }

        public static IEnumerable<Type> GetTypesWithInterface(this Assembly assembly, Type interfaceType)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (interfaceType == null)
            {
                throw new ArgumentNullException(nameof(interfaceType));
            }

            return assembly.GetLoadableTypes()
                .Where(interfaceType.IsAssignableFrom)
                .ToList();
        }
    }
}
