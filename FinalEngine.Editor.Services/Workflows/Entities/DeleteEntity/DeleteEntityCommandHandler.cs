namespace FinalEngine.Editor.Services.Workflows.Entities.DeleteEntity
{
    using System;
    using FinalEngine.ECS;
    using FinalEngine.Editor.Services.Instructions;
    using FinalEngine.Editor.Services.Instructions.Entities;
    using FluentValidation;
    using MediatR;
    using Micky5991.EventAggregator.Interfaces;
    using Microsoft.Extensions.Logging;

    public class DeleteEntityCommandHandler : RequestHandler<DeleteEntityCommand>
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IInstructionsManager instructionsManager;

        private readonly ILogger<DeleteEntityCommandHandler> logger;

        private readonly IValidator<DeleteEntityCommand> validator;

        private readonly IEntityWorld world;

        public DeleteEntityCommandHandler(
            ILogger<DeleteEntityCommandHandler> logger,
            IValidator<DeleteEntityCommand> validator,
            IEventAggregator eventAggregator,
            IInstructionsManager instructionsManager,
            IEntityWorld world)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.instructionsManager = instructionsManager ?? throw new ArgumentNullException(nameof(instructionsManager));
            this.world = world ?? throw new ArgumentNullException(nameof(world));
        }

        protected override void Handle(DeleteEntityCommand request)
        {
            this.logger.LogInformation($"Deleting entity with tag: '{request.Entity.Tag}' and ID: '{request.Entity.Identifier}'...");

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            this.validator.ValidateAndThrow(request);

            var instruction = new RemoveEntityInstruction(
                this.eventAggregator,
                this.world,
                request.Entity);

            this.instructionsManager.PerformInstruction(instruction);
            this.logger.LogInformation("Entity removed from world.");
        }
    }
}
