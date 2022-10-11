#nullable disable warnings

namespace FinalEngine.Editor.Services.Workflows.Entities.CreateEntity
{
    using System;
    using MediatR;

    public class CreateEntityCommand : IRequest
    {
        public Guid Identifier { get; set; }

        public string Tag { get; set; }
    }
}
