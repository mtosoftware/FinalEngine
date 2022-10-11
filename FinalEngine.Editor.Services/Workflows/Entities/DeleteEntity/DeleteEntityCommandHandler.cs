namespace FinalEngine.Editor.Services.Workflows.Entities.DeleteEntity
{
    using System;
    using FinalEngine.ECS;
    using FinalEngine.Editor.Services.Instructions;
    using FinalEngine.Editor.Services.Instructions.Entities;
    using MediatR;
    using Micky5991.EventAggregator.Interfaces;
    using Microsoft.Extensions.Logging;

    public class DeleteEntityCommandHandler : RequestHandler<DeleteEntityCommand>
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IInstructionsManager instructionsManager;

        private readonly ILogger<DeleteEntityCommandHandler> logger;

        private readonly IEntityWorld world;

        public DeleteEntityCommandHandler(
            ILogger<DeleteEntityCommandHandler> logger,
            IEventAggregator eventAggregator,
            IInstructionsManager instructionsManager,
            IEntityWorld world)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.instructionsManager = instructionsManager ?? throw new ArgumentNullException(nameof(instructionsManager));
            this.world = world ?? throw new ArgumentNullException(nameof(world));
        }

        protected override void Handle(DeleteEntityCommand request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            this.logger.LogInformation($"Deleting entity with tag: '{request.Entity.Tag}' and ID: '{request.Entity.Identifier}'");

            var entity = request.Entity;

            var instruction = new RemoveEntityInstruction(
                this.eventAggregator,
                this.world,
                entity);

            instruction.Execute();

            this.instructionsManager.AddInstruction(instruction);

            this.logger.LogInformation("Entity removed from world.");
        }
    }
}
