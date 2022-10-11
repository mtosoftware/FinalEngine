namespace FinalEngine.Editor.Services.Workflows.Components.AddComponent
{
    using FinalEngine.ECS;
    using MediatR;

    public class AddComponentCommand : IRequest
    {
        public IComponent Component { get; set; }

        public Entity Entity { get; set; }
    }
}
