namespace FinalEngine.Editor.Services.Workflows.Instructions.Undo
{
    using System;
    using FinalEngine.Editor.Services.Instructions;
    using MediatR;

    public class UndoCommandHandler : RequestHandler<UndoCommand>
    {
        private readonly IInstructionsManager instructionsManager;

        public UndoCommandHandler(IInstructionsManager instructionsManager)
        {
            this.instructionsManager = instructionsManager ?? throw new ArgumentNullException(nameof(instructionsManager));
        }

        protected override void Handle(UndoCommand request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            this.instructionsManager.Undo();
        }
    }
}
