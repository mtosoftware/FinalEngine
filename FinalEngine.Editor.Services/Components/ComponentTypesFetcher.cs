namespace FinalEngine.Editor.Services.Components
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using FinalEngine.ECS;
    using FinalEngine.Editor.Services.Extensions;

    public class ComponentTypesFetcher : IComponentTypesFetcher
    {
        private static IComponentTypesFetcher instance;

        public static IComponentTypesFetcher Instance
        {
            get { return instance ??= new ComponentTypesFetcher(); }
        }

        public IEnumerable<Type> FetchComponentTypes(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return assembly.GetTypesWithInterface(typeof(IComponent));
        }
    }
}
