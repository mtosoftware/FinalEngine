namespace FinalEngine.Editor.Services.Instructions.Components
{
    using System;
    using FinalEngine.ECS;

    public class AddComponentInstruction : IInstruction
    {
        private readonly IComponent component;

        private readonly Entity entity;

        public AddComponentInstruction(Entity entity, IComponent component)
        {
            this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
            this.component = component ?? throw new ArgumentNullException(nameof(component));
        }

        public void Execute()
        {
            this.entity.AddComponent(this.component);
        }

        public void UnExecute()
        {
            this.entity.RemoveComponent(this.component);
        }
    }
}
