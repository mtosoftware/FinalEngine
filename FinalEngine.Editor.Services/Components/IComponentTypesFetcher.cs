namespace FinalEngine.Editor.Services.Components
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IComponentTypesFetcher
    {
        IEnumerable<Type> FetchComponentTypes(Assembly assembly);
    }
}
