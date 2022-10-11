namespace FinalEngine.Editor.Services.Workflows.Entities.CreateEntity
{
    using System;
    using FinalEngine.ECS;
    using FinalEngine.Editor.Services.Instructions;
    using FinalEngine.Editor.Services.Instructions.Entities;
    using FluentValidation;
    using MediatR;
    using Micky5991.EventAggregator.Interfaces;
    using Microsoft.Extensions.Logging;

    public class CreateEntityCommandHandler : RequestHandler<CreateEntityCommand>
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IInstructionsManager instructionsManager;

        private readonly ILogger<CreateEntityCommandHandler> logger;

        private readonly IValidator<CreateEntityCommand> validator;

        private readonly IEntityWorld world;

        public CreateEntityCommandHandler(
            ILogger<CreateEntityCommandHandler> logger,
            IEventAggregator eventAggregator,
            IInstructionsManager instructionsManager,
            IEntityWorld world,
            IValidator<CreateEntityCommand> validator)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.instructionsManager = instructionsManager ?? throw new ArgumentNullException(nameof(instructionsManager));
            this.world = world ?? throw new ArgumentNullException(nameof(world));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        protected override void Handle(CreateEntityCommand request)
        {
            this.logger.LogInformation($"Creating entity with tag: '{request.Tag}' and ID: '{request.Identifier}'");

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            this.validator.ValidateAndThrow(request);

            var entity = new Entity(request.Identifier)
            {
                Tag = request.Tag,
            };

            var instruction = new AddEntityInstruction(
                this.eventAggregator,
                this.world,
                entity);

            this.instructionsManager.PerformInstruction(instruction);
            this.logger.LogInformation("Entity added to world.");
        }
    }
}
