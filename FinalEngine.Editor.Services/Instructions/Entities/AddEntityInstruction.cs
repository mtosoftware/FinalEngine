namespace FinalEngine.Editor.Services.Instructions.Entities
{
    using System;
    using FinalEngine.ECS;
    using FinalEngine.Editor.Services.Events.Entities;
    using Micky5991.EventAggregator.Interfaces;

    public class AddEntityInstruction : IInstruction
    {
        private readonly Entity entity;

        private readonly IEventAggregator eventAggregator;

        private readonly IEntityWorld world;

        public AddEntityInstruction(
            IEventAggregator eventAggregator,
            IEntityWorld world,
            Entity entity)
        {
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.world = world ?? throw new ArgumentNullException(nameof(world));
            this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        public void Execute()
        {
            this.world.AddEntity(this.entity);
            this.eventAggregator.Publish(new EntityAddedEvent(this.entity));
        }

        public void UnExecute()
        {
            this.world.RemoveEntity(this.entity);
            this.eventAggregator.Publish(new EntityRemovedEvent()
            {
                Identifier = this.entity.Identifier,
            });
        }
    }
}
