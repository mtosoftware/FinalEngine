namespace FinalEngine.Editor.Services.Events.Entities
{
    using System;
    using Micky5991.EventAggregator.Elements;

    public class EntityRemovedEvent : EventBase
    {
        public Guid Identifier { get; init; }
    }
}
