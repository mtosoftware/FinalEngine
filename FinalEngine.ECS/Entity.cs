// <copyright file="Entity.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;

    /// <summary>
    ///   Provides a container for <see cref="IComponent"/> s that can be, added, removed or accessed during runtime through an <see cref="EntitySystemBase"/>.
    /// </summary>
    /// <seealso cref="DynamicObject"/>
    /// <seealso cref="IReadOnlyEntity"/>
    public class Entity : DynamicObject, IReadOnlyEntity
    {
        /// <summary>
        ///   Represents a type to <see cref="IComponent"/> map.
        /// </summary>
        private readonly IDictionary<Type, IComponent> typeToComponentMap;

        /// <summary>
        ///   Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="identifier">
        ///   The globally unique identifier for this <see cref="Entity"/>.
        /// </param>
        public Entity(Guid identifier = default)
        {
            this.Identifier = identifier == default ? Guid.NewGuid() : default;
            this.typeToComponentMap = new Dictionary<Type, IComponent>();
        }

        /// <summary>
        ///   Gets the identifier.
        /// </summary>
        /// <value>
        ///   The identifier.
        /// </value>
        public Guid Identifier { get; }

        /// <summary>
        ///   Gets or sets the tag.
        /// </summary>
        /// <value>
        ///   The tag.
        /// </value>
        public string? Tag { get; set; }

        /// <summary>
        ///   Gets or sets the event that occurs when a component is added or removed from this <see cref="Entity"/>.
        /// </summary>
        /// <value>
        ///   The event that occurs when a component is added or removed from this <see cref="Entity"/>.
        /// </value>
        internal EventHandler<EventArgs>? OnComponentsChanged { get; set; }

        /// <summary>
        ///   Adds the specified <paramref name="component"/> to this <see cref="Entity"/>.
        /// </summary>
        /// <param name="component">
        ///   The component to add to this <see cref="Entity"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="component"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="component"/> parameters type has already been added to this entity.
        /// </exception>
        public void AddComponent(IComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component), $"The specified {nameof(component)} parameter cannot be null.");
            }

            var type = component.GetType();

            if (this.ContainsComponent(type))
            {
                throw new ArgumentException($"The specified {nameof(component)} parameters type has already been added to this entity", nameof(component));
            }

            this.typeToComponentMap.Add(type, component);
            this.OnComponentsChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///   Instantiates and adds the specified <typeparamref name="TComponent"/> to this <see cref="Entity"/>.
        /// </summary>
        /// <typeparam name="TComponent">
        ///   The type component to add to this <see cref="Entity"/>.
        /// </typeparam>
        public void AddComponent<TComponent>()
            where TComponent : IComponent, new()
        {
            this.AddComponent(Activator.CreateInstance<TComponent>());
        }

        /// <summary>
        ///   Determines whether the specified <paramref name="component"/> is contained within this <see cref="Entity"/>.
        /// </summary>
        /// <param name="component">
        ///   The component to check.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="component"/> is contained within this <see cref="Entity"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="component"/> parameter cannot be null.
        /// </exception>
        public bool ContainsComponent(IComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component), $"The specified {nameof(component)} parameter cannot be null.");
            }

            foreach (var other in this.typeToComponentMap.Values)
            {
                if (other == component)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///   Determines whether a component of the specified <paramref name="type"/> is contained within this <see cref="Entity"/>.
        /// </summary>
        /// <param name="type">
        ///   The type of component to check.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the a component of the specified <paramref name="type"/> is contained within this <see cref="Entity"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   type - The specified <paramref name="type"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="type"/> parameter does not implement <see cref="IComponent"/>.
        /// </exception>
        public bool ContainsComponent(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type), $"The specified {nameof(type)} parameter cannot be null.");
            }

            return !typeof(IComponent).IsAssignableFrom(type)
                ? throw new ArgumentException($"The specified {nameof(type)} parameter does not implement {nameof(IComponent)}.", nameof(type))
                : this.typeToComponentMap.ContainsKey(type);
        }

        /// <summary>
        ///   Determines whether a component of the specified <typeparamref name="TComponent"/> is contained within this <see cref="Entity"/>.
        /// </summary>
        /// <typeparam name="TComponent">
        ///   The type of component to check.
        /// </typeparam>
        /// <returns>
        ///   <c>true</c> if a component of the specified <typeparamref name="TComponent"/> is contained within this <see cref="Entity"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsComponent<TComponent>()
            where TComponent : IComponent
        {
            return this.ContainsComponent(typeof(TComponent));
        }

        /// <summary>
        ///   Gets a component of the specified <paramref name="type"/> from this <see cref="Entity"/>.
        /// </summary>
        /// <param name="type">
        ///   The type of component to retrieve.
        /// </param>
        /// <returns>
        ///   The component of the specified <paramref name="type"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="type"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="type"/> parameter does not implement <see cref="IComponent"/> or the specified <paramref name="type"/> parameter is not a component type that has been added to this <see cref="Entity"/>.
        /// </exception>
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

            return !this.ContainsComponent(type)
                ? throw new ArgumentException($"The specified {nameof(type)} parameter is not a component type that has been added to this entity.", nameof(type))
                : this.typeToComponentMap[type];
        }

        /// <summary>
        ///   Gets a component of the specified <typeparamref name="TComponent"/> from this <see cref="Entity"/>.
        /// </summary>
        /// <typeparam name="TComponent">
        ///   The type of component to retrieve.
        /// </typeparam>
        /// <returns>
        ///   The component of the specified <typeparamref name="TComponent"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///   The specified <typeparamref name="TComponent"/> parameter is not a component type that has been added to this <see cref="Entity"/>.
        /// </exception>
        public TComponent GetComponent<TComponent>()
            where TComponent : IComponent
        {
            return (TComponent)this.GetComponent(typeof(TComponent));
        }

        /// <summary>
        ///   Removes the specified <paramref name="component"/> from this <see cref="Entity"/>.
        /// </summary>
        /// <param name="component">
        ///   The component to remove.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="component"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="component"/> parameter has not been added to this entity.
        /// </exception>
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

        /// <summary>
        ///   Removes a component of the specified <paramref name="type"/> from this <see cref="Entity"/>.
        /// </summary>
        /// <param name="type">
        ///   The type of component to remove.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   type - The specified <paramref name="type"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="type"/> parameter does not implement <see cref="IComponent"/> or the specified <paramref name="type"/> parameter is not a component type that has been added to this entity.
        /// </exception>
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

        /// <summary>
        ///   Removes a component of the specified <typeparamref name="TComponent"/> from this <see cref="Entity"/>.
        /// </summary>
        /// <typeparam name="TComponent">
        ///   The type of the component to remove.
        /// </typeparam>
        /// <exception cref="ArgumentException">
        ///   A component of the specified <typeparamref name="TComponent"/> parameter has not been added to this entity.
        /// </exception>
        public void RemoveComponent<TComponent>()
            where TComponent : IComponent
        {
            this.RemoveComponent(typeof(TComponent));
        }

        /// <summary>
        ///   Converts to string.
        /// </summary>
        /// <returns>
        ///   A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Tag ?? string.Empty;
        }

        /// <summary>
        ///   Attempts to retrieve a component by class name.
        /// </summary>
        /// <param name="binder">
        ///   Provides information about the object that called the dynamic operation. The <c>binder.Name</c> property provides the name of the member on which the dynamic operation is performed. For example, for the <c>Console.WriteLine(sampleObject.SampleProperty)</c> statement, where <c>sampleObject</c> is an instance of the class derived from the <see cref="DynamicObject"/> class, <c>binder.Name</c> returns "SampleProperty". The <c>binder.IgnoreCase</c> property specifies whether the member name is case-sensitive.
        /// </param>
        /// <param name="result">
        ///   The component that has been retrieved; otherwise, <c>null</c>.
        /// </param>
        /// <returns>
        ///   <see langword="true"/> if the operation is successful; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   binder - The specified <paramref name="binder"/> parameter cannot be null.
        /// </exception>
        /// <remarks>
        ///   If the name of the component you wish to access has a suffix of `Component`, you can choose whether to omit it when retrieving the component. See the example for further explanation.
        /// </remarks>
        /// <example>
        ///   <code>
        /// // Create an entity and add a component.
        /// dynamic entity = new Entity();
        /// var component = new ExampleComponent();
        ///
        /// entity.AddComponent(component);
        ///
        /// // You can then retrieve the component like so:
        /// var example = entity.GetComponent(typeof(ExampleComponent));
        ///
        /// // Or just the generic version for compile time stuff.
        /// var example = entity.GetComponent&lt;ExampleComponent&gt;();
        ///
        /// // Or even access it dynamically via it's class name.
        /// ExampleComponent example = entity.ExampleComponent;
        ///
        /// // Lastly, you can make it shorter by dropping the suffix.
        /// ExampleComponent example = entity.Example;</code>
        /// </example>
        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            if (binder == null)
            {
                throw new ArgumentNullException(nameof(binder), $"The specified {nameof(binder)} parameter cannot be null.");
            }

            string? memberName = binder.Name;

            foreach (var kvp in this.typeToComponentMap)
            {
                string? typeName = kvp.Key.Name;

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
