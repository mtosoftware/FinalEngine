// <copyright file="Entity.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;

    public sealed class Entity : DynamicObject, IReadOnlyEntity
    {
        internal EventHandler<EventArgs>? OnComponentsChanged;

        private readonly IDictionary<Type, IComponent> typeToComponentMap;

        public Entity()
        {
            this.typeToComponentMap = new Dictionary<Type, IComponent>();
        }

        public void AddComponent(IComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component), $"The specified {nameof(component)} parameter cannot be null.");
            }

            Type type = component.GetType();

            if (this.ContainsComponent(type))
            {
                throw new ArgumentException($"The specified {nameof(component)} parameters type has already been added to this entity", nameof(component));
            }

            this.typeToComponentMap.Add(type, component);
            this.OnComponentsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddComponent<TComponent>()
            where TComponent : IComponent, new()
        {
            this.AddComponent(Activator.CreateInstance<TComponent>());
        }

        public bool ContainsComponent(IComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component), $"The specified {nameof(component)} parameter cannot be null.");
            }

            foreach (IComponent other in this.typeToComponentMap.Values)
            {
                if (other == component)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsComponent(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type), $"The specified {nameof(type)} parameter cannot be null.");
            }

            if (!typeof(IComponent).IsAssignableFrom(type))
            {
                throw new ArgumentException($"The specified {nameof(type)} parameter does not implement {nameof(IComponent)}.", nameof(type));
            }

            return this.typeToComponentMap.ContainsKey(type);
        }

        public bool ContainsComponent<TComponent>()
            where TComponent : IComponent
        {
            return this.ContainsComponent(typeof(TComponent));
        }

        public IComponent GetComponent(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type), $"The specified {nameof(type)} parameter cannot be null.");
            }

            if (!typeof(IComponent).IsAssignableFrom(type))
            {
                throw new ArgumentException($"The specified {nameof(type)} parameter does not implement {nameof(IComponent)}.", nameof(type));
            }

            if (!this.ContainsComponent(type))
            {
                throw new ArgumentException($"The specified {nameof(type)} parameter is not a component type that has been added to this entity.", nameof(type));
            }

            return this.typeToComponentMap[type];
        }

        public TComponent GetComponent<TComponent>()
            where TComponent : IComponent
        {
            return (TComponent)this.GetComponent(typeof(TComponent));
        }

        public void RemoveComponent(IComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component), $"The specified {nameof(component)} parameter cannot be null.");
            }

            if (!this.ContainsComponent(component))
            {
                throw new ArgumentException($"The specified {nameof(component)} parameter has not been added to this entity.", nameof(component));
            }

            this.RemoveComponent(component.GetType());
        }

        public void RemoveComponent(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type), $"The specified {nameof(type)} parameter cannot be null.");
            }

            if (!typeof(IComponent).IsAssignableFrom(type))
            {
                throw new ArgumentException($"The specified {nameof(type)} parameter does not implement {nameof(IComponent)}.", nameof(type));
            }

            if (!this.ContainsComponent(type))
            {
                throw new ArgumentException($"The specified {nameof(type)} parameter is not a component type that has been added to this entity.", nameof(type));
            }

            this.typeToComponentMap.Remove(type);
            this.OnComponentsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveComponent<TComponent>()
            where TComponent : IComponent
        {
            this.RemoveComponent(typeof(TComponent));
        }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            if (binder == null)
            {
                throw new ArgumentNullException(nameof(binder), $"The specified {nameof(binder)} parameter cannot be null.");
            }

            string memberName = binder.Name;

            foreach (KeyValuePair<Type, IComponent> kvp in this.typeToComponentMap)
            {
                string typeName = kvp.Key.Name;

                if (typeName.EndsWith("Component", StringComparison.CurrentCulture))
                {
                    typeName = typeName.Remove(typeName.Length - "Component".Length);
                }

                if (typeName == memberName)
                {
                    result = kvp.Value;
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}