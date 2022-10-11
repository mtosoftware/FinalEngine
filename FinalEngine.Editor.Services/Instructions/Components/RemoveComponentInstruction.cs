namespace FinalEngine.Editor.Services.Instructions.Components
{
    using System;
    using FinalEngine.ECS;

    public class RemoveComponentInstruction : IInstruction
    {
        private readonly IComponent component;

        private readonly Entity entity;

        public RemoveComponentInstruction(Entity entity, IComponent component)
        {
            this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
            this.component = component ?? throw new ArgumentNullException(nameof(component));
        }

        public void Execute()
        {
            this.entity.RemoveComponent(this.component);
        }

        public void UnExecute()
        {
            this.entity.AddComponent(this.component);
        }
    }
}
