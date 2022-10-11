namespace FinalEngine.Editor.Services.Events.Entities
{
    using System;
    using FinalEngine.ECS;
    using Micky5991.EventAggregator.Elements;

    public class EntityAddedEvent : EventBase
    {
        public EntityAddedEvent(Entity entity)
        {
            this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        public Entity Entity { get; }
    }
}
