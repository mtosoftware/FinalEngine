namespace FinalEngine.Editor.Services.Workflows.Components.RemoveComponent
{
    using FinalEngine.ECS;
    using MediatR;

    public class RemoveComponentCommand : IRequest
    {
        public IComponent Component { get; }

        public Entity Entity { get; }
    }
}
