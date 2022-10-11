#nullable disable warnings

namespace FinalEngine.Editor.Services.Workflows.Entities.DeleteEntity
{
    using FinalEngine.ECS;
    using MediatR;

    public class DeleteEntityCommand : IRequest
    {
        public Entity Entity { get; set; }
    }
}
